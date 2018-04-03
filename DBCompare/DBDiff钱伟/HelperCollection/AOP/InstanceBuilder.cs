using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.AOP
{
    /// <summary>
    /// 生产 AOP 实例
    /// </summary>
    public class InstanceBuilder
    {
        private const TypeAttributes TYPE_ATTRIBUTES = TypeAttributes.Public | TypeAttributes.Class;
        private const MethodAttributes METHOD_ATTRIBUTES = MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual ;
        private const PropertyAttributes PROPERTY_ATTRIBUTES = PropertyAttributes.HasDefault ;

        //生产的新类型和旧类型的map
        private static ConcurrentDictionary<Type, Type> TypeMapCache = new ConcurrentDictionary<Type, Type>();

        public static Type GetProxyType(Type instanceType)
        {
            Type proxyType = null;
            if (!TypeMapCache.TryGetValue(instanceType, out proxyType))
            {
                proxyType = BuilderType(instanceType);
                TypeMapCache.TryAdd(instanceType, proxyType);
            }
            return proxyType;
        }
        //缓存暂时没有实
        public static T CreateInstance<T>(params object[] parameters) where T : class, new()
        {
            Type baseType = typeof(T);
            Type proxyType = GetProxyType(baseType);
            //Type proxyType = null;
            //if(!TypeMapCache.TryGetValue(baseType, out proxyType))
            //{
            //    proxyType = BuilderType(baseType);
            //    TypeMapCache.TryAdd(baseType, proxyType);
            //}
           // return (T)Activator.CreateInstance(proxyType, parameters);
            return (T)FastObjectCreator.CreateObject(proxyType, parameters);
        }

        public static T CreateInstance<T>() where T : class, new()
        {
            return CreateInstance<T>(null);
        }
   
        private static Type BuilderType(Type baseType)
        {
            string strAssemblyName = baseType.Name + ".AOP";
            string fileName = strAssemblyName + ".dll";
            AssemblyName an = new AssemblyName { Name = strAssemblyName };
           // an.SetPublicKey(Assembly.GetExecutingAssembly().GetName().GetPublicKey());
            AssemblyBuilder assembly = AppDomain.CurrentDomain.DefineDynamicAssembly(an, AssemblyBuilderAccess.RunAndSave);//然其可以保存方便以后直接引用DLL
            ModuleBuilder module = assembly.DefineDynamicModule(strAssemblyName, fileName);
            TypeBuilder typeBuilder = module.DefineType(baseType.Name+"_AOP", TYPE_ATTRIBUTES, baseType);
            BuildConstructor(baseType, typeBuilder);
            BuildMethod(baseType, typeBuilder);
            BuildProperty(baseType, typeBuilder);
            Type type = typeBuilder.CreateType();

         //   assembly.Save(fileName); //  暂时不保存

            return type;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="typeBuilder"></param>
        private static void BuildConstructor(Type baseType, TypeBuilder typeBuilder)
        {
            foreach (var ctor in baseType.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
            {
                var parameterTypes = ctor.GetParameters().Select(u => u.ParameterType).ToArray();
                var ctorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public|MethodAttributes.HideBySig, CallingConventions.Standard, parameterTypes);

                ILGenerator il = ctorBuilder.GetILGenerator();
                for (int i = 0; i <= parameterTypes.Length; ++i)
                {
                    LoadArgument(il, i);
                }
                il.Emit(OpCodes.Call, ctor);//调用对应的Base的构造函数
                il.Emit(OpCodes.Ret);
            }
        }
        /// <summary>
        /// LoadParameter
        /// </summary>
        /// <param name="il"></param>
        /// <param name="index"></param>
        private static void LoadArgument(ILGenerator il, int index)
        {
            switch (index)
            {
                case 0:
                    il.Emit(OpCodes.Ldarg_0);
                    break;
                case 1:
                    il.Emit(OpCodes.Ldarg_1);
                    break;
                case 2:
                    il.Emit(OpCodes.Ldarg_2);
                    break;
                case 3:
                    il.Emit(OpCodes.Ldarg_3);
                    break;
                default:
                    if (index <= 127)
                    {
                        il.Emit(OpCodes.Ldarg_S, index);
                    }
                    else
                    {
                        il.Emit(OpCodes.Ldarg, index);
                    }
                    break;
            }
        }

        private static void OverrideProperty(PropertyInfo propertyInfo, TypeBuilder typeBuilder)
        {
            object[] attrs = propertyInfo.GetCustomAttributes(typeof(CallHandlerAttribute), true);
            if (attrs.Length == 0) { return; }
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(propertyInfo.Name, PropertyAttributes.HasDefault, propertyInfo.PropertyType, null);
            MethodBuilder SetMethod = OverrideMethod(propertyInfo.SetMethod, typeBuilder, attrs);
            MethodBuilder GetMethod = OverrideMethod(propertyInfo.GetMethod, typeBuilder, attrs);
            if (GetMethod != null)
            {
                propertyBuilder.SetGetMethod(GetMethod);
            }
            if (SetMethod != null)
            {
                propertyBuilder.SetSetMethod(SetMethod);
            }
           // ttt(propertyInfo, typeBuilder);
        }
        private static MethodBuilder OverrideMethod(MethodInfo methodInfo, TypeBuilder typeBuilder)
        {
            object[] attrs = methodInfo.GetCustomAttributes(typeof(CallHandlerAttribute), true);
            return  OverrideMethod(methodInfo, typeBuilder, attrs);
        }
        /// <summary>
        /// 核心的overide 方法(为了以后兼容配置文件方式)
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="typeBuilder"></param>
        /// <param name="attrs"></param>
        private static MethodBuilder OverrideMethod(MethodInfo methodInfo, TypeBuilder typeBuilder, object[] attrs)
        {
            if (!methodInfo.IsVirtual && !methodInfo.IsAbstract) return null;
            int attrCount = attrs.Length;
            if (attrCount == 0) return null;

            ParameterInfo[] parameters = methodInfo.GetParameters();
            Type[] parameterTypes = parameters.Select(u => u.ParameterType).ToArray();

            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                methodInfo.Name,
                METHOD_ATTRIBUTES,
                methodInfo.ReturnType,
                parameterTypes);
            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
            for (int i = 0; i < parameters.Length; i++)
            {
                methodBuilder.DefineParameter(i + 1, parameters[i].Attributes, parameters[i].Name);
            }
            methodBuilder.SetParameters(parameterTypes);
            ILGenerator il = methodBuilder.GetILGenerator();
            LocalBuilder localContext = il.DeclareLocal(typeof(MethodContext));
            #region init context
            il.Emit(OpCodes.Newobj, typeof(MethodContext).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc, localContext);
            il.Emit(OpCodes.Ldloc, localContext);
            il.Emit(OpCodes.Ldstr, methodInfo.Name );
            il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_MethodName"), new[] { typeof(string) });
            il.Emit(OpCodes.Ldloc, localContext);
            il.Emit(OpCodes.Ldstr, methodInfo.DeclaringType.Name);
            il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_ClassName"), new[] { typeof(string) });
            il.Emit(OpCodes.Ldloc, localContext);
            il.Emit(OpCodes.Ldarg_0);
            il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_Executor"), new[] { typeof(object) });
            #endregion

            #region context.Parameters = new object[Length];
            LocalBuilder tmpParameters = il.DeclareLocal(typeof(object[]));
            il.Emit(OpCodes.Ldc_I4, parameters.Length);
            il.Emit(OpCodes.Newarr, typeof(object));
            il.Emit(OpCodes.Stloc, tmpParameters);
            for (int i = 0; i < parameters.Length; ++i)
            {
                il.Emit(OpCodes.Ldloc, tmpParameters);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldarg, i + 1);//第0个是其本身（this）
                il.Emit(OpCodes.Box, parameterTypes[i]);
                // il.EmitCall(OpCodes.Callvirt, typeof(object).GetMethod("GetType", new Type[] { }), null); //直接存值
                il.Emit(OpCodes.Stelem_Ref);
            }
            il.Emit(OpCodes.Ldloc, localContext);//  do not know
            il.Emit(OpCodes.Ldloc, tmpParameters);
            il.EmitCall(OpCodes.Callvirt, typeof(MethodContext).GetMethod("set_Parameters"), new[] { typeof(object[]) });
            #endregion

            //处理返回值（定义但是未初始化）
            LocalBuilder localReturnValue = null;
            if (methodInfo.ReturnType != typeof(void)) // has return value
            {
                localReturnValue = il.DeclareLocal(methodInfo.ReturnType);
                //初始化
                if (methodInfo.ReturnType.IsValueType)
                {
                
                }
                else
                {
                    il.Emit(OpCodes.Ldnull);
                    il.Emit(OpCodes.Stloc, localReturnValue);
                }
            }
            // ICallHandler[] handlers = new ICallHandler[attrCount];
            LocalBuilder localHandlers = il.DeclareLocal(typeof(ICallHandler[]));
            il.Emit(OpCodes.Ldc_I4, attrCount);
            il.Emit(OpCodes.Newarr, typeof(ICallHandler));
            il.Emit(OpCodes.Stloc, localHandlers);

            #region create ICallHandler instance
            for (int i = 0; i < attrCount; ++i)
            {
                LocalBuilder currentCallHandler = il.DeclareLocal(typeof(ICallHandler));
                CallHandlerAttribute attr = (attrs[i] as CallHandlerAttribute);
                //MethodInfo GetCallHandler = attr.GetType().GetMethod("GetCallHandler");
                //ConstructorInfo constructorInfo = attr.GetType().GetConstructor(Type.EmptyTypes);
                //if (constructorInfo == null) { throw new NullReferenceException("ICallHandler 必须要有一个无参构造"); }
                //il.Emit(OpCodes.Newobj, constructorInfo);
                //il.Emit(OpCodes.Callvirt, GetCallHandler);

                ICallHandler ICallHandler = attr.GetCallHandler();
                ConstructorInfo constructorInfo = ICallHandler.GetType().GetConstructor(Type.EmptyTypes);
                if (constructorInfo == null) { throw new NullReferenceException("ICallHandler 必须要有一个无参构造"); }
                il.Emit(OpCodes.Newobj, constructorInfo);
                il.Emit(OpCodes.Stloc, currentCallHandler);
                il.Emit(OpCodes.Ldloc, localHandlers);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldloc, currentCallHandler);
                il.Emit(OpCodes.Stelem_Ref);
            }
            #endregion

            // BeginInvoke
            for (int i = 0; i < attrCount; ++i)
            {
                il.Emit(OpCodes.Ldloc, localHandlers);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloc, localContext);
               // il.Emit(OpCodes.Ldnull);
                il.Emit(OpCodes.Callvirt, typeof(ICallHandler).GetMethod("BeginInvoke"));
            }

            Label endLabel = il.DefineLabel(); // if (context.Processed) goto: ...
            il.Emit(OpCodes.Ldloc, localContext);
            il.EmitCall(OpCodes.Callvirt, typeof(MethodContext).GetMethod("get_Processed"), Type.EmptyTypes);
            il.Emit(OpCodes.Ldc_I4_1);
            il.Emit(OpCodes.Beq, endLabel);
            //il.Emit(OpCodes.Brtrue_S, endLabel);

            // excute base method
            LocalBuilder localException = il.DeclareLocal(typeof(Exception));

            //开始try
            il.BeginExceptionBlock(); // try {

            // il.Emit(OpCodes.Ldloc, localContext);

            il.Emit(OpCodes.Ldarg_0);
            for (int i = 0; i < parameterTypes.Length; ++i)
            {
                LoadArgument(il, i + 1);
            }
            il.EmitCall(OpCodes.Call, methodInfo, parameterTypes);
            // is has return value, save it
            if (methodInfo.ReturnType != typeof(void))
            {
                if (methodInfo.ReturnType.IsValueType)
                {
                    il.Emit(OpCodes.Box, methodInfo.ReturnType);
                }
                il.Emit(OpCodes.Stloc, localReturnValue);

                il.Emit(OpCodes.Ldloc, localContext);
                il.Emit(OpCodes.Ldloc, localReturnValue);
                il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_ReturnValue"), new[] { typeof(object) });
            }
            //发生错误
            il.BeginCatchBlock(typeof(Exception)); // } catch {
            // OnException
            il.Emit(OpCodes.Stloc, localException);
            il.Emit(OpCodes.Ldloc, localContext);

            il.Emit(OpCodes.Ldloc, localException);
            il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_Exception"), new[] { typeof(Exception) });

            for (int i = 0; i < attrCount; ++i)
            {
                il.Emit(OpCodes.Ldloc, localHandlers);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloc, localContext);

                il.Emit(OpCodes.Callvirt, typeof(ICallHandler).GetMethod("OnException"));
            }
            // il.BeginFinallyBlock();//Finnaly

            // EndInvoke

            il.EndExceptionBlock(); // }
            // end excute base method
            il.MarkLabel(endLabel);
            for (int i = 0; i < attrCount; ++i)
            {
                il.Emit(OpCodes.Ldloc, localHandlers);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_Ref);
                il.Emit(OpCodes.Ldloc, localContext);
                il.Emit(OpCodes.Callvirt, typeof(ICallHandler).GetMethod("EndInvoke"));
            }

            if (methodInfo.ReturnType != typeof(void))
            {
                il.Emit(OpCodes.Ldloc, localReturnValue);
                il.Emit(OpCodes.Unbox_Any, methodInfo.ReturnType);//上面装过箱所以这边拆了下
            }
            else
            {
                //il.Emit(OpCodes.Ldnull);  //没有的话不加在直接返回  不然属性会报错
            }

            il.Emit(OpCodes.Ret);

            return methodBuilder;
        }

        private static void BuildMethod(Type baseType, TypeBuilder typeBuilder)
        {
            foreach (var methodInfo in baseType.GetMethods())
            {
                OverrideMethod(methodInfo, typeBuilder);
                //下面的代码被提到新的方法中去了方法名 OverrideMethod
                //if (!methodInfo.IsVirtual && !methodInfo.IsAbstract) continue;
                //object[] attrs = methodInfo.GetCustomAttributes(typeof(CallHandlerAttribute), true);
                //int attrCount = attrs.Length;
                //if (attrCount == 0) continue;

                //ParameterInfo[] parameters = methodInfo.GetParameters();
                //Type[] parameterTypes = parameters.Select(u => u.ParameterType).ToArray();

                //MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                //    methodInfo.Name,
                //    METHOD_ATTRIBUTES,
                //    methodInfo.ReturnType,
                //    parameterTypes);

                //for (int i = 0; i < parameters.Length; i++)
                //{
                //    methodBuilder.DefineParameter(i + 1, parameters[i].Attributes, parameters[i].Name);
                //}
                //methodBuilder.SetParameters(parameterTypes);

                //ILGenerator il = methodBuilder.GetILGenerator();

                //// MethodContext context = new MethodContext();
                //LocalBuilder localContext = il.DeclareLocal(typeof(MethodContext));

                //#region init context
                //il.Emit(OpCodes.Newobj, typeof(MethodContext).GetConstructor(Type.EmptyTypes));
                //il.Emit(OpCodes.Stloc, localContext);
                //// context.MethodName = m.Name;
                //il.Emit(OpCodes.Ldloc, localContext);
                //il.Emit(OpCodes.Ldstr, methodInfo.Name);
                //il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_MethodName"), new[] { typeof(string) });
                //// context.ClassName = m.DeclaringType.Name;
                //il.Emit(OpCodes.Ldloc, localContext);
                //il.Emit(OpCodes.Ldstr, methodInfo.DeclaringType.Name);
                //il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_ClassName"), new[] { typeof(string) });
                //// context.Executor = this;
                //il.Emit(OpCodes.Ldloc, localContext);
                //il.Emit(OpCodes.Ldarg_0);
                //il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_Executor"), new[] { typeof(object) });
                //#endregion

                //// set context.Parameters
                //#region context.Parameters = new object[Length];
                //LocalBuilder tmpParameters = il.DeclareLocal(typeof(object[]));
                //il.Emit(OpCodes.Ldc_I4, parameters.Length);
                //il.Emit(OpCodes.Newarr, typeof(object));
                //il.Emit(OpCodes.Stloc, tmpParameters);
                //for (int i = 0; i < parameters.Length; ++i)
                //{
                //    il.Emit(OpCodes.Ldloc, tmpParameters);
                //    il.Emit(OpCodes.Ldc_I4, i);
                //    il.Emit(OpCodes.Ldarg, i + 1);
                //    il.Emit(OpCodes.Box, parameterTypes[i]);
                //    il.EmitCall(OpCodes.Call, typeof(object).GetMethod("GetType", new Type[] { }), null);
                //    il.Emit(OpCodes.Stelem_Ref);
                //}
                //il.Emit(OpCodes.Ldloc, localContext);
                //il.Emit(OpCodes.Ldloc, tmpParameters);
                //il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_Parameters"), new[] { typeof(object[]) });
                //#endregion

                //LocalBuilder localReturnValue = null;
                //if (methodInfo.ReturnType != typeof(void)) // has return value
                //{
                //    localReturnValue = il.DeclareLocal(methodInfo.ReturnType);
                //}

                //// ICallHandler[] handlers = new ICallHandler[attrCount];
                //LocalBuilder localHandlers = il.DeclareLocal(typeof(ICallHandler[]));
                //il.Emit(OpCodes.Ldc_I4, attrCount);
                //il.Emit(OpCodes.Newarr, typeof(ICallHandler));
                //il.Emit(OpCodes.Stloc, localHandlers);

                //// create ICallHandler instance
                //#region create ICallHandler instance
                //for (int i = 0; i < attrCount; ++i)
                //{
                //    //LocalBuilder tmpNameValueCollection = il.DeclareLocal(typeof(NameValueCollection));
                //    //il.Emit(OpCodes.Newobj, typeof(NameValueCollection).GetConstructor(Type.EmptyTypes));
                //    //il.Emit(OpCodes.Stloc, tmpNameValueCollection);

                //    LocalBuilder currentCallHandler = il.DeclareLocal(typeof(ICallHandler[]));
                //    CallHandlerAttribute attr = (attrs[i] as CallHandlerAttribute);
                //    MethodInfo GetCallHandler = attr.GetType().GetMethod("GetCallHandler");
                //    il.Emit(OpCodes.Newobj, attr.GetType().GetConstructor(Type.EmptyTypes));
                //    il.Emit(OpCodes.Callvirt, GetCallHandler);
                //    il.Emit(OpCodes.Stloc, currentCallHandler);
                //    il.Emit(OpCodes.Ldloc, localHandlers);
                //    il.Emit(OpCodes.Ldc_I4, i);
                //    il.Emit(OpCodes.Ldloc, currentCallHandler);
                //    il.Emit(OpCodes.Stelem_Ref);
                //    //NameValueCollection attrCollection = attr.GetAttrs();
                //    //foreach (var key in attrCollection.AllKeys)
                //    //{
                //    //    il.Emit(OpCodes.Ldloc, tmpNameValueCollection);
                //    //    il.Emit(OpCodes.Ldstr, key);
                //    //    il.Emit(OpCodes.Ldstr, attrCollection[key]);
                //    //    il.Emit(OpCodes.Callvirt, typeof(NameValueCollection).GetMethod("Add", new[] { typeof(string), typeof(string) }));
                //    //}

                //    //il.Emit(OpCodes.Ldloc, localHandlers);
                //    //il.Emit(OpCodes.Ldc_I4, i);
                //    //il.Emit(OpCodes.Ldloc, tmpNameValueCollection);
                //    //il.Emit(OpCodes.Newobj, attr.CallHandlerType.GetConstructor(new[] { typeof(NameValueCollection) }));
                //    //il.Emit(OpCodes.Stelem_Ref);
                //}
                //#endregion

                //// BeginInvoke
                //for (int i = 0; i < attrCount; ++i)
                //{
                //    il.Emit(OpCodes.Ldloc, localHandlers);
                //    il.Emit(OpCodes.Ldc_I4, i);
                //    il.Emit(OpCodes.Ldelem_Ref);
                //    il.Emit(OpCodes.Ldloc, localContext);
                //    il.Emit(OpCodes.Callvirt, typeof(ICallHandler).GetMethod("BeginInvoke"));
                //}

                //Label endLabel = il.DefineLabel(); // if (context.Processed) goto: ...
                //il.Emit(OpCodes.Ldloc, localContext);
                //il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("get_Processed"), Type.EmptyTypes);
                //il.Emit(OpCodes.Ldc_I4_1);
                //il.Emit(OpCodes.Beq, endLabel);

                //// excute base method
                //LocalBuilder localException = il.DeclareLocal(typeof(Exception));
                //il.BeginExceptionBlock(); // try {

                //il.Emit(OpCodes.Ldloc, localContext);

                //il.Emit(OpCodes.Ldarg_0);
                //for (int i = 0; i < parameterTypes.Length; ++i)
                //{
                //    LoadArgument(il, i + 1);
                //}
                //il.EmitCall(OpCodes.Call, methodInfo, parameterTypes);
                //// is has return value, save it
                //if (methodInfo.ReturnType != typeof(void))
                //{
                //    if (methodInfo.ReturnType.IsValueType)
                //    {
                //        il.Emit(OpCodes.Box, methodInfo.ReturnType);
                //    }
                //    il.Emit(OpCodes.Stloc, localReturnValue);

                //    il.Emit(OpCodes.Ldloc, localContext);
                //    il.Emit(OpCodes.Ldloc, localReturnValue);
                //    il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_ReturnValue"), new[] { typeof(object) });
                //}

                //il.BeginCatchBlock(typeof(Exception)); // } catch {
                //// OnException
                //il.Emit(OpCodes.Stloc, localException);
                //il.Emit(OpCodes.Ldloc, localContext);
                //il.Emit(OpCodes.Ldloc, localException);
                //il.EmitCall(OpCodes.Call, typeof(MethodContext).GetMethod("set_Exception"), new[] { typeof(Exception) });

                //for (int i = 0; i < attrCount; ++i)
                //{
                //    il.Emit(OpCodes.Ldloc, localHandlers);
                //    il.Emit(OpCodes.Ldc_I4, i);
                //    il.Emit(OpCodes.Ldelem_Ref);
                //    il.Emit(OpCodes.Ldloc, localContext);
                //    il.Emit(OpCodes.Callvirt, typeof(ICallHandler).GetMethod("OnException"));
                //}

                //il.EndExceptionBlock(); // }
                //// end excute base method

                //il.MarkLabel(endLabel);

                //// EndInvoke
                //for (int i = 0; i < attrCount; ++i)
                //{
                //    il.Emit(OpCodes.Ldloc, localHandlers);
                //    il.Emit(OpCodes.Ldc_I4, i);
                //    il.Emit(OpCodes.Ldelem_Ref);
                //    il.Emit(OpCodes.Ldloc, localContext);
                //    il.Emit(OpCodes.Callvirt, typeof(ICallHandler).GetMethod("EndInvoke"));
                //}

                //if (methodInfo.ReturnType != typeof(void))
                //{
                //    il.Emit(OpCodes.Ldloc, localReturnValue);
                //}
                //else
                //{
                //    il.Emit(OpCodes.Ldnull);
                //}

                //il.Emit(OpCodes.Ret);
            }
        }
        private static void BuildProperty(Type baseType, TypeBuilder typeBuilder)
        {
            foreach (PropertyInfo pi in baseType.GetProperties())
            {
                OverrideProperty(pi, typeBuilder);
            }
        }
    }
}
