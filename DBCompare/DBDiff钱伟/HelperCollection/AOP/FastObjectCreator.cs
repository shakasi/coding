using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.AOP
{

    public static class FastObjectCreator
    {
        private delegate object CreateOjectHandler(object[] parameters);

        private static ConcurrentDictionary<string, CreateOjectHandler> creatorCache = new ConcurrentDictionary<string, CreateOjectHandler>();

        /// <summary>
        /// CreateObject
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object CreateObject(Type type, params object[] parameters)
        {
            Type[] parameterTypes = GetParameterTypes(parameters);
            string id = GetIdentity(type, parameterTypes);
            CreateOjectHandler ctor = null;
            if (!creatorCache.TryGetValue(id, out ctor))
            {
                ctor = CreateHandler(type, parameterTypes);
                creatorCache.TryAdd(id, ctor);
            }
            return ctor.Invoke(parameters);
        }

        /// <summary>
        /// 获取唯一标识 （用于缓存）
        /// </summary>
        private static string GetIdentity(Type type, Type[] parameterTypes)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(type.Assembly.FullName);
            sb.Append(type.FullName);
            foreach (Type t in parameterTypes)
            {
                sb.Append(t.Assembly.FullName);
                sb.Append(t.FullName);
            }
            return sb.ToString();
        }

        /// <summary>
        /// CreateHandler
        /// </summary>
        /// <param name="type"></param>
        /// <param name="paramsTypes"></param>
        /// <returns></returns>
        private static CreateOjectHandler CreateHandler(Type type, Type[] paramsTypes)
        {
            DynamicMethod method = new DynamicMethod("DynamicCreateOject", typeof(object),
                new Type[] { typeof(object[]) }, typeof(CreateOjectHandler).Module);

            ConstructorInfo constructor = type.GetConstructor(paramsTypes);

            ILGenerator il = method.GetILGenerator();

            for (int i = 0; i < paramsTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldc_I4, i);
                il.Emit(OpCodes.Ldelem_Ref);
                if (paramsTypes[i].IsValueType)
                {
                    il.Emit(OpCodes.Unbox_Any, paramsTypes[i]);
                }
                else
                {
                    il.Emit(OpCodes.Castclass, paramsTypes[i]);
                }
            }
            il.Emit(OpCodes.Newobj, constructor);
            il.Emit(OpCodes.Ret);

            return (CreateOjectHandler)method.CreateDelegate(typeof(CreateOjectHandler));
        }

        /// <summary>
        /// GetParameterTypes
        /// </summary>
        /// <param name="token"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static Type[] GetParameterTypes(params object[] parameters)
        {
            if (parameters == null) return new Type[0];
            Type[] values = new Type[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                values[i] = parameters[i].GetType();
            }
            return values;
        }
    }
}

