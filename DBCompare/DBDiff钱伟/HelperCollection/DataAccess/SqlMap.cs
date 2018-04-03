using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using  System.Collections.Concurrent;
using Utility.HelperCollection.DataAccess.Attribute;
using System.Data.Common;
using Utility.HelperCollection.DataAccess.DataBase;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Utility.HelperCollection.DataAccess
{
    public static class SqlMap<T> where T : EntityBase, new()
    {    
         private static readonly  Dictionary<Type, DbType> typeMap;
         static SqlMap()
        {
            typeMap = new Dictionary<Type, DbType>();
            typeMap[typeof(byte)] = DbType.Byte;
            typeMap[typeof(sbyte)] = DbType.SByte;
            typeMap[typeof(short)] = DbType.Int16;
            typeMap[typeof(ushort)] = DbType.UInt16;
            typeMap[typeof(int)] = DbType.Int32;
            typeMap[typeof(uint)] = DbType.UInt32;
            typeMap[typeof(long)] = DbType.Int64;
            typeMap[typeof(ulong)] = DbType.UInt64;
            typeMap[typeof(float)] = DbType.Single;
            typeMap[typeof(double)] = DbType.Double;
            typeMap[typeof(decimal)] = DbType.Decimal;
            typeMap[typeof(bool)] = DbType.Boolean;
            typeMap[typeof(string)] = DbType.String;
            typeMap[typeof(char)] = DbType.StringFixedLength;
            typeMap[typeof(Guid)] = DbType.Guid;
            typeMap[typeof(DateTime)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
            typeMap[typeof(TimeSpan)] = DbType.Time;
            typeMap[typeof(byte[])] = DbType.Binary;
            typeMap[typeof(byte?)] = DbType.Byte;
            typeMap[typeof(sbyte?)] = DbType.SByte;
            typeMap[typeof(short?)] = DbType.Int16;
            typeMap[typeof(ushort?)] = DbType.UInt16;
            typeMap[typeof(int?)] = DbType.Int32;
            typeMap[typeof(uint?)] = DbType.UInt32;
            typeMap[typeof(long?)] = DbType.Int64;
            typeMap[typeof(ulong?)] = DbType.UInt64;
            typeMap[typeof(float?)] = DbType.Single;
            typeMap[typeof(double?)] = DbType.Double;
            typeMap[typeof(decimal?)] = DbType.Decimal;
            typeMap[typeof(bool?)] = DbType.Boolean;
            typeMap[typeof(char?)] = DbType.StringFixedLength;
            typeMap[typeof(Guid?)] = DbType.Guid;
            typeMap[typeof(DateTime?)] = DbType.DateTime;
            typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
            typeMap[typeof(TimeSpan?)] = DbType.Time;
            typeMap[typeof(object)] = DbType.Object;
          }

        #region Static Readonly Fields
        private static readonly MethodInfo DataRecord_ItemGetter_Int =
            typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo DataRecord_GetOrdinal =
            typeof(IDataRecord).GetMethod("GetOrdinal");
        private static readonly MethodInfo DataReader_Read =
            typeof(IDataReader).GetMethod("Read");
        private static readonly MethodInfo Convert_IsDBNull =
            typeof(Convert).GetMethod("IsDBNull");
        private static readonly MethodInfo DataRecord_GetDateTime =
            typeof(IDataRecord).GetMethod("GetDateTime");
        private static readonly MethodInfo DataRecord_GetDecimal =
            typeof(IDataRecord).GetMethod("GetDecimal");
        private static readonly MethodInfo DataRecord_GetDouble =
            typeof(IDataRecord).GetMethod("GetDouble");
        private static readonly MethodInfo DataRecord_GetInt32 =
            typeof(IDataRecord).GetMethod("GetInt32");
        private static readonly MethodInfo DataRecord_GetInt64 =
            typeof(IDataRecord).GetMethod("GetInt64");
        private static readonly MethodInfo DataRecord_GetString =
            typeof(IDataRecord).GetMethod("GetString");
        private static readonly MethodInfo DataRecord_IsDBNull =
            typeof(IDataRecord).GetMethod("IsDBNull");

        #endregion

        #region Init Methods
        private static Converter<IDataReader, List<T>> CreateBatchDataLoader(List<DbColumnInfo> columnInfoes,bool useAop)
        {
            DynamicMethod dm = new DynamicMethod(string.Empty, typeof(List<T>),
                new Type[] { typeof(IDataReader) }, typeof(SqlMap<T>));
            ILGenerator il = dm.GetILGenerator();
            LocalBuilder list = il.DeclareLocal(typeof(List<T>));
            LocalBuilder item = il.DeclareLocal(typeof(T));
            Label exit = il.DefineLabel();
            Label loop = il.DefineLabel();
            // List<T> list = new List<T>();
            il.Emit(OpCodes.Newobj, typeof(List<T>).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_S, list);
            // [ int %index% = arg.GetOrdinal(%ColumnName%); ]
            LocalBuilder[] colIndices = GetColumnIndices(il, columnInfoes);
            // while (arg.Read()) {
            il.MarkLabel(loop);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Call, DataReader_Read);
            il.Emit(OpCodes.Brfalse, exit);
            //      T item = new T { %Property% =  };
            BuildItem(il, columnInfoes, item, colIndices, useAop);
            //      list.Add(item);
            il.Emit(OpCodes.Ldloc_S, list);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Callvirt, typeof(List<T>).GetMethod("Add"));
            // }
            il.Emit(OpCodes.Br, loop);
            il.MarkLabel(exit);
            // return list;
            il.Emit(OpCodes.Ldloc_S, list);
            il.Emit(OpCodes.Ret);
            return (Converter<IDataReader, List<T>>)dm.CreateDelegate(typeof(Converter<IDataReader, List<T>>));
        }

        private static LocalBuilder[] GetColumnIndices(ILGenerator il, List<DbColumnInfo> columnInfoes)
        {
            LocalBuilder[] colIndices = new LocalBuilder[columnInfoes.Count];
            for (int i = 0; i < colIndices.Length; i++)
            {
                // int %index% = arg.GetOrdinal(%ColumnName%);
                colIndices[i] = il.DeclareLocal(typeof(int));
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldstr, columnInfoes[i].ColumnName);
                il.Emit(OpCodes.Callvirt, DataRecord_GetOrdinal);
                il.Emit(OpCodes.Stloc_S, colIndices[i]);
            }
            return colIndices;
        }

        //private static void BuildItem(ILGenerator il, List<DbColumnInfo> columnInfoes,
        //    LocalBuilder item, LocalBuilder[] colIndices,)
        //{
        //    BuildItem(il, columnInfoes, item, colIndices, true);//默认使用AOP
        //}
        private static void BuildItem(ILGenerator il, List<DbColumnInfo> columnInfoes,
            LocalBuilder item, LocalBuilder[] colIndices,bool useAOP)
        {
            if (useAOP)
            {
                //下面那段代码是为了AOP  代替直接实例化
                //T aop = Utility.HelperCollection.AOP.InstanceBuilder.CreateInstance<T>();
                //il.Emit(OpCodes.Newobj, aop.GetType().GetConstructor(Type.EmptyTypes));
                Type proxyType= Utility.HelperCollection.AOP.InstanceBuilder.GetProxyType(typeof(T));
                ConstructorInfo constructorInfo=proxyType.GetConstructor(Type.EmptyTypes);
                if (constructorInfo == null) { throw new NullReferenceException("entity必须存在一个空构造"); }
                il.Emit(OpCodes.Newobj,constructorInfo );
            }
            else
            {
                // T item = new T();
                ConstructorInfo constructorInfo = typeof(T).GetConstructor(Type.EmptyTypes);
                if (constructorInfo == null) { throw new NullReferenceException("entity必须存在一个空构造"); }
                il.Emit(OpCodes.Newobj, constructorInfo);
            }

            il.Emit(OpCodes.Stloc_S, item);
            //设置状态是
            for (int i = 0; i < colIndices.Length; i++)
            {
                    if (IsCompatibleType(columnInfoes[i].Type, typeof(int)))
                    {
                        // item.%Property% = arg.GetInt32(%index%);
                        ReadInt32(il, item, columnInfoes, colIndices, i);
                    }
                    else if (IsCompatibleType(columnInfoes[i].Type, typeof(int?)))
                    {
                        // item.%Property% = arg.IsDBNull ? default(int?) : (int?)arg.GetInt32(%index%);
                        ReadNullableInt32(il, item, columnInfoes, colIndices, i);
                    }
                    else if (IsCompatibleType(columnInfoes[i].Type, typeof(long)))
                    {
                        // item.%Property% = arg.GetInt64(%index%);
                        ReadInt64(il, item, columnInfoes, colIndices, i);
                    }
                    else if (IsCompatibleType(columnInfoes[i].Type, typeof(long?)))
                    {
                        // item.%Property% = arg.IsDBNull ? default(long?) : (long?)arg.GetInt64(%index%);
                        ReadNullableInt64(il, item, columnInfoes, colIndices, i);
                    }
                    else if (IsCompatibleType(columnInfoes[i].Type, typeof(decimal)))
                    {
                        // item.%Property% = arg.GetDecimal(%index%);
                        ReadDecimal(il, item, columnInfoes[i].SetMethod, colIndices[i]);
                    }
                    else if (columnInfoes[i].Type == typeof(decimal?))
                    {
                        // item.%Property% = arg.IsDBNull ? default(decimal?) : (int?)arg.GetDecimal(%index%);
                        ReadNullableDecimal(il, item, columnInfoes[i].SetMethod, colIndices[i]);
                    }
                    else if (columnInfoes[i].Type == typeof(DateTime))
                    {
                        // item.%Property% = arg.GetDateTime(%index%);
                        ReadDateTime(il, item, columnInfoes[i].SetMethod, colIndices[i]);
                    }
                    else if (columnInfoes[i].Type == typeof(DateTime?))
                    {
                        // item.%Property% = arg.IsDBNull ? default(DateTime?) : (int?)arg.GetDateTime(%index%);
                        ReadNullableDateTime(il, item, columnInfoes[i].SetMethod, colIndices[i]);
                    }
                    else
                    {
                         // item.%Property% = (%PropertyType%)arg[%index%];
                          ReadObject(il, item, columnInfoes, colIndices, i);
                    }
            }
            if (useAOP)
            {
                //需要重新设置一些属性    EntityState.UnChange
                MethodInfo BaseEntity_SetEntityState = typeof(T).GetProperty("EntityState", typeof(EntityState)).GetSetMethod();
                if (BaseEntity_SetEntityState != null)
                {
                    il.Emit(OpCodes.Ldloc_S, item);
                    il.Emit(OpCodes.Ldc_I4_S, (int)EntityState.UnChange);
                    il.Emit(OpCodes.Call, BaseEntity_SetEntityState);
                }
            
            }
        }

        private static bool IsCompatibleType(Type t1, Type t2)
        {
            if (t1 == t2)
                return true;
            if (t1.IsEnum && Enum.GetUnderlyingType(t1) == t2)
                return true;
            var u1 = Nullable.GetUnderlyingType(t1);
            var u2 = Nullable.GetUnderlyingType(t2);
            if (u1 != null && u2 != null)
                return IsCompatibleType(u1, u2);
            return false;
        }

        private static void ReadInt32(ILGenerator il, LocalBuilder item,
            List<DbColumnInfo> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt32);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        private static void ReadNullableInt32(ILGenerator il, LocalBuilder item,
            List<DbColumnInfo> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            var local = il.DeclareLocal(columnInfoes[i].Type);
            Label intNull = il.DefineLabel();
            Label intCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, intNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt32);
            il.Emit(OpCodes.Call, columnInfoes[i].Type.GetConstructor(
                new Type[] { Nullable.GetUnderlyingType(columnInfoes[i].Type) }));
            il.Emit(OpCodes.Br_S, intCommon);
            il.MarkLabel(intNull);
            il.Emit(OpCodes.Initobj, columnInfoes[i].Type);
            il.MarkLabel(intCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        private static void ReadInt64(ILGenerator il, LocalBuilder item,
            List<DbColumnInfo> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt64);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        private static void ReadNullableInt64(ILGenerator il, LocalBuilder item,
            List<DbColumnInfo> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            var local = il.DeclareLocal(columnInfoes[i].Type);
            Label intNull = il.DefineLabel();
            Label intCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, intNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_GetInt64);
            il.Emit(OpCodes.Call, columnInfoes[i].Type.GetConstructor(
                new Type[] { Nullable.GetUnderlyingType(columnInfoes[i].Type) }));
            il.Emit(OpCodes.Br_S, intCommon);
            il.MarkLabel(intNull);
            il.Emit(OpCodes.Initobj, columnInfoes[i].Type);
            il.MarkLabel(intCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        private static void ReadDecimal(ILGenerator il, LocalBuilder item,
            MethodInfo setMethod, LocalBuilder colIndex)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDecimal);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        private static void ReadNullableDecimal(ILGenerator il, LocalBuilder item,
            MethodInfo setMethod, LocalBuilder colIndex)
        {
            var local = il.DeclareLocal(typeof(decimal?));
            Label decimalNull = il.DefineLabel();
            Label decimalCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, decimalNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDecimal);
            il.Emit(OpCodes.Call, typeof(decimal?).GetConstructor(new Type[] { typeof(decimal) }));
            il.Emit(OpCodes.Br_S, decimalCommon);
            il.MarkLabel(decimalNull);
            il.Emit(OpCodes.Initobj, typeof(decimal?));
            il.MarkLabel(decimalCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        private static void ReadDateTime(ILGenerator il, LocalBuilder item,
            MethodInfo setMethod, LocalBuilder colIndex)
        {
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDateTime);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        private static void ReadNullableDateTime(ILGenerator il, LocalBuilder item,
            MethodInfo setMethod, LocalBuilder colIndex)
        {
            var local = il.DeclareLocal(typeof(DateTime?));
            Label dtNull = il.DefineLabel();
            Label dtCommon = il.DefineLabel();
            il.Emit(OpCodes.Ldloca, local);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_IsDBNull);
            il.Emit(OpCodes.Brtrue_S, dtNull);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndex);
            il.Emit(OpCodes.Callvirt, DataRecord_GetDateTime);
            il.Emit(OpCodes.Call, typeof(DateTime?).GetConstructor(new Type[] { typeof(DateTime) }));
            il.Emit(OpCodes.Br_S, dtCommon);
            il.MarkLabel(dtNull);
            il.Emit(OpCodes.Initobj, typeof(DateTime?));
            il.MarkLabel(dtCommon);
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldloc, local);
            il.Emit(OpCodes.Callvirt, setMethod);
        }

        private static void ReadObject(ILGenerator il, LocalBuilder item,
            List<DbColumnInfo> columnInfoes, LocalBuilder[] colIndices, int i)
        {
            Label common = il.DefineLabel();
            il.Emit(OpCodes.Ldloc_S, item);
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldloc_S, colIndices[i]);
            il.Emit(OpCodes.Callvirt, DataRecord_ItemGetter_Int);
            il.Emit(OpCodes.Dup);
            il.Emit(OpCodes.Call, Convert_IsDBNull);
            il.Emit(OpCodes.Brfalse_S, common);
            il.Emit(OpCodes.Pop);
            il.Emit(OpCodes.Ldnull);
            il.MarkLabel(common);
            il.Emit(OpCodes.Unbox_Any, columnInfoes[i].Type);
            il.Emit(OpCodes.Callvirt, columnInfoes[i].SetMethod);
        }

        #endregion

        #region Private Method
        private static DbTableInfo CreateDbTableInfoForType(Type t)
        {
            DbTableInfo result = new DbTableInfo();
            if (!DbTableInforChache.TryGetValue(t.GetType(), out result))
            {
                DbTableInfo dbTable = new DbTableInfo();
                List<DbColumnInfo> lst = new List<DbColumnInfo>();
                SqlTableAttribute attr = (SqlTableAttribute)t.GetCustomAttribute(typeof(SqlTableAttribute));
                if (attr != null)
                {
                    dbTable.TableName = attr.TableName;
                }
                SqlPrimaryKeyAttribute attr1 = (SqlPrimaryKeyAttribute)t.GetCustomAttribute(typeof(SqlPrimaryKeyAttribute));
                if (attr1 != null)
                {
                    dbTable.PrimaryKey = attr1.Key;
                }
                PropertyInfo[] pis = t.GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    DbColumnInfo dci = DbColumnInfo.BuildInstanceForPropertyInfo(pi);
                    if (!string.IsNullOrEmpty(dci.ColumnName))
                    {
                        lst.Add(dci);
                    }
                }
                dbTable.Columns = lst;
                DbTableInforChache.TryAdd(t.GetType(), dbTable);
                return dbTable;
            }
            return result;
        }

        /// <summary>
        /// Converter From entity to a dictionary
        /// </summary>
        /// <returns></returns>
        private static Converter<T, Dictionary<string,object>> CreateKVForEntity()
        {

            DynamicMethod dm = new DynamicMethod(string.Empty, typeof(Dictionary<string, object>),
                new Type[] { typeof(T) }, typeof(SqlMap<T>));
            ILGenerator il = dm.GetILGenerator();
            LocalBuilder result = il.DeclareLocal(typeof(Dictionary<string, object>));
            il.Emit(OpCodes.Newobj, typeof(Dictionary<string, object>).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_S, result);
            List<DbColumnInfo> lst = GetDbTableInfoByType(typeof(T)).Columns;
            MethodInfo m = typeof(Dictionary<string, object>).GetMethod("Add");
            foreach (DbColumnInfo dbcli in lst)
            {
                il.Emit(OpCodes.Ldloc_S, result);
                il.Emit(OpCodes.Ldstr, dbcli.PropertyName);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, dbcli.GetMethod);
                if (dbcli.Type.IsValueType)
                {
                    il.Emit(OpCodes.Box, dbcli.Type);
                }
                il.Emit(OpCodes.Callvirt, m);
            }
            il.Emit(OpCodes.Ldloc_S, result);
            il.Emit(OpCodes.Ret);
            return (Converter<T, Dictionary<string, object>>)dm.CreateDelegate(typeof(Converter<T, Dictionary<string, object>>));
        }


        //下面的方法是数据库的方法(以后可以拉到不同的类中间) 
      

        #endregion

        #region Cache 
        //缓存
        //private static ConcurrentDictionary<Type, Converter<IDataReader, List<T>>> EntityConverterCache = new ConcurrentDictionary<Type, Converter<IDataReader, List<T>>>();
        private static ConcurrentDictionary<string, Converter<IDataReader, List<T>>> EntityConverterCache = new ConcurrentDictionary<string, Converter<IDataReader, List<T>>>();
        //缓存类型对应的 列的信息
       // private static ConcurrentDictionary<Type, List<DbColumnInfo>> DbColumnInfoCache = new ConcurrentDictionary<Type, List<DbColumnInfo>>();
        //缓存类型某个 类型对应的表结构信息
        private static ConcurrentDictionary<Type, DbTableInfo> DbColumnInfoCache = new ConcurrentDictionary<Type, DbTableInfo>();
        //缓存 对象转字典
        private static ConcurrentDictionary<Type, Converter<T, Dictionary<string, object>>> EntityToDictionaryCache = new ConcurrentDictionary<Type, Converter<T, Dictionary<string, object>>>();

        private static ConcurrentDictionary<Type, DbTableInfo> DbTableInforChache = new ConcurrentDictionary<Type, DbTableInfo>();
        

        #endregion

        #region 生产SQL语句(目前只以Sqlserver为例子)

        private static DbCommand BuildSimpleSelectCommand(IDataBase database)
        {
            //  IDataBase database = DataBaseFactory.CreateDataBase(DataBaseType.MSSQLServer);
            DbCommand cmd = database.CreateCommand();
            DbTableInfo tableInfo = CreateDbTableInfoForType(typeof(T));
            string tableName = tableInfo.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new NullReferenceException("没有对象没有申明表名");
            }
            //可能可以做修改只选在DbTableInfo中有列的
            string selectCommand = string.Format("select * from {0}", tableName);
            cmd.CommandText = selectCommand;
            return cmd;
        }
        private static DbCommand BuildSelectCommand( DbSearchConditionCollection<T> condition,IDataBase database)
        {
            //  IDataBase database = DataBaseFactory.CreateDataBase(DataBaseType.MSSQLServer);
            DbCommand cmd = BuildSimpleSelectCommand(database);
            Dictionary<string, object> dic = new Dictionary<string, object>();
            foreach (DbSearchConditionInfo info in condition.Conditions)
            {
                dic.Add(info.Field, info.FieldData);
            }
            AppendWhereConditionForCommand(cmd, database, dic.Keys.ToArray(), dic, WhereConditionOperation.Append);
            return cmd;
           // throw new NotImplementedException("to do");
        }
        private static DbCommand BuildSelectCommand(T condition, IDataBase database)
        {
            return BuildSelectCommand(condition, null, database);
        }
        private static DbCommand BuildSelectCommand(T condition, List<string> conditionFields, IDataBase database)
        {
            DbCommand cmd = BuildSimpleSelectCommand(database);
           // string whereSql = "";
            if (condition != null)
            {
                string[] fields;
                Dictionary<string, object> entityValues = GetEntityToDictionaryConverter()(condition);
                if (conditionFields == null)
                {
                   // fields = condition.GetChangedFields();   //如果是New的永远是空的
                    // fields = entityValues.Keys.ToArray(); //取所有的
                    //下面的代码是解决要查询的 列是null的
                    //由于状态时New的所有_OriginalData一开始是空的，当赋值的时候才会添加进去查询的时候我们用_OriginalData来判断哪些是查询条件（后续可以修改）
                    if (condition.EntityState != EntityState.New)
                    {
                        throw new NotSupportedException("未声明查询列的时候对象必须是New状态");
                    }
                    fields = condition.GetOriginalData().Keys.ToArray(); //这个方法能直接获取值可能后边的entityValues有点多余
                }
                else
                {
                    fields = conditionFields.ToArray();
                }

                AppendWhereConditionForCommand(cmd, database, fields, entityValues,WhereConditionOperation.Append);
                //foreach (KeyValuePair<string, object> kv in parameters)
                //{
                //    if (kv.Value != null && fields.Contains(kv.Key))
                //    {
                //        DbParameter param = database.CreateParameter();
                //        param.Direction = ParameterDirection.Input;
                //        param.ParameterName = database.DbParameterPrefix + kv.Key;
                //        param.Value = kv.Value;
                //        param.DbType = typeMap[kv.Value.GetType()];
                //        cmd.Parameters.Add(param);
                //    }
                //}
            }
           // cmd.CommandText = selectCommand + whereSql;
            return cmd;
        }
        /// <summary>
        /// 弃用
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="database"></param>
        /// <param name="fields"></param>
        /// <param name="kvs"></param>
        private static void AppendWhereConditionForCommand(DbCommand cmd, IDataBase database, string[] fields, Dictionary<string, object> kvs)
        {
            AppendWhereConditionForCommand(cmd, database, fields, kvs, WhereConditionOperation.Wrap);//默认是包装原来的CommandText
        }
        //需要重构这个方法    将string[] fields, Dictionary<string, object> kvs这个条件改为一个    DbSearchConditionCollection<T> condition
        /// <summary>
        /// 弃用
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="database"></param>
        /// <param name="fields"></param>
        /// <param name="kvs"></param>
        /// <param name="conditionOP"></param>
        private static void AppendWhereConditionForCommand(DbCommand cmd, IDataBase database, string[] fields, Dictionary<string, object> kvs, WhereConditionOperation conditionOP)
        {
            if (fields.Length > 0 && !string.IsNullOrEmpty(cmd.CommandText))
            {
                string[] whereCondition = new string[fields.Length];

                for (int i = 0; i < fields.Length; i++)
                {

                    if (kvs.ContainsKey(fields[i]))
                    {

                        if (kvs[fields[i]] == null)//如果不是值类型而且是null的则认为是DBNull
                        {
                            whereCondition[i] = string.Format("{0} is null ", fields[i]);
                        }
                        else
                        {
                            whereCondition[i] = string.Format("{0}={1}{0}", fields[i], database.DbParameterPrefix);
                            DbParameter param = database.CreateParameter();
                            param.Direction = ParameterDirection.Input;
                            param.ParameterName = database.DbParameterPrefix + fields[i];
                            param.Value = kvs[fields[i]];
                            param.DbType = typeMap[kvs[fields[i]].GetType()];
                            cmd.Parameters.Add(param);
                        }

                    }
                    else
                    {
                        throw new NullReferenceException("无法获取" + fields[i] + "的值");
                    }
                }

               string whereSql = " Where " + string.Join(" AND ", whereCondition);//where 语句
               if (WhereConditionOperation.Wrap == conditionOP)
               {
                    cmd.CommandText = string.Format("select * from ({0}) {1}",cmd.CommandText,whereSql);
               }
               else if (WhereConditionOperation.Append == conditionOP)
               {
                   cmd.CommandText = cmd.CommandText + whereSql;
               }
               else
               {
                   throw new NotSupportedException("内部错误！无效的WhereConditionOperation！");
               }
            }
        }
        private static DbCommand BuildUpdateCommand(T condition, IDataBase database)
        {
            //IDataBase database = DataBaseFactory.CreateDataBase(DataBaseType.MSSQLServer);
            DbCommand cmd = database.CreateCommand();
            DbTableInfo tableInfo = CreateDbTableInfoForType(typeof(T));
            string tableName = tableInfo.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new NullReferenceException("该对象没有申明表名");
            }
            if (tableInfo.PrimaryKey == null || tableInfo.PrimaryKey.Length == 0)
            {
                throw new NullReferenceException("该对象没有申明主键无法生产Update命令");
            }
            if (condition.EntityState != EntityState.Modified && condition.GetOrigianlEntityState() != EntityState.UnChange)
            {
                throw new NotSupportedException("该对象的状态不支持Update命令");
            }
            if (condition != null)
            {
                string[] fields = condition.GetChangedFields();
                if (fields.Length == 0)
                {
                    throw new NullReferenceException("没有需要跟新的字段");
                }
                //Update 
                string strSql = " Update " + tableInfo.TableName;//生产的Update语句

                //set
                Dictionary<string, object> entityValues = GetEntityToDictionaryConverter()(condition);
                string[] setCondition = new string[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    setCondition[i] = string.Format(" {0}={1}{0}", fields[i], database.DbParameterPrefix);
                    DbParameter param = database.CreateParameter();
                    param.Direction = ParameterDirection.Input;
                    param.ParameterName = database.DbParameterPrefix + fields[i];
                    if (entityValues[fields[i]] == null)
                    {
                        param.Value = DBNull.Value;
                    }
                    else
                    {
                        param.Value = entityValues[fields[i]];
                        param.DbType = typeMap[entityValues[fields[i]].GetType()];
                    }
                    cmd.Parameters.Add(param);
                }
                string setSql =" Set "+ string.Join(" AND ", setCondition);
                //where
                string[] pks = tableInfo.PrimaryKey;
                string[] whereCondition = new string[pks.Length];
                Dictionary<string,object> pkValues=  condition.GetOriginalData(pks);
                if (pkValues.Count == pks.Length)
                {
                    for(int i =0;i<pks.Length;i++)
                    {
                        whereCondition[i] = string.Format(" {0}={1}{0}", pks[i], database.DbParameterPrefix);
                        DbParameter param = database.CreateParameter();
                        param.Direction = ParameterDirection.Input;
                        param.ParameterName = database.DbParameterPrefix + pks[i];
                        if (pkValues[pks[i]] == null)
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            param.Value = pkValues[pks[i]];
                            param.DbType = typeMap[pkValues[pks[i]].GetType()];
                        }
                        cmd.Parameters.Add(param);
                    }
                }
                else
                {
                    throw new NotSupportedException("无效的唯一键！");
                }
                string whereSql = " Where " + string.Join(" AND ", whereCondition);

                string sql = strSql + setSql + whereSql;
                cmd.CommandText = sql;
            }
            else
            {
                throw new NotSupportedException("Update命令不能作用于空对象"); 
            }
            return cmd;
        }
        private static DbCommand BuildInsertCommand(T condition, IDataBase database)
        {
            if (condition == null)
            {
                throw new NotSupportedException("insert 操作实体不能为空！");
            }
            if (condition.EntityState != EntityState.New || condition.GetOrigianlEntityState() != EntityState.New)
            {
                throw new NotSupportedException("该对象的状态不支持insert操作！");
            }
            //IDataBase database = DataBaseFactory.CreateDataBase(DataBaseType.MSSQLServer);
            DbCommand cmd = database.CreateCommand();
            DbTableInfo tableInfo = CreateDbTableInfoForType(typeof(T));
            string tableName = tableInfo.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new NullReferenceException("没有对象没有申明表名");
            }
            string insertCommand = string.Format("insert into {0}", tableName);
           // if (condition != null)  //上面做了判断了一次注释掉
            {

                Dictionary<string, object> fieldValues = condition.GetOriginalData(); //获取所有的修改获得数据
                if (fieldValues.Count > 0)
                {
                  //  Dictionary<string, object> entityValues = GetEntityToDictionaryConverter()(condition);
                    string[] valueCondition = new string[fieldValues.Count];
                    int i=0;
                    foreach (KeyValuePair<string,object> kv in fieldValues)
                    {
                        valueCondition[i] = string.Format("{0}{1}", database.DbParameterPrefix,kv.Key );
                        DbParameter param = database.CreateParameter();
                        param.Direction = ParameterDirection.Input;
                        param.ParameterName = database.DbParameterPrefix + kv.Key;
                        if (kv.Value== null)
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            param.Value = kv.Value;
                            param.DbType = typeMap[kv.Key.GetType()];
                        }
                        cmd.Parameters.Add(param);
                        i++;
                    }
                    cmd.CommandText = insertCommand + "(" + string.Join(",", fieldValues.Keys.ToArray()) + ") values (" + string.Join(",", valueCondition) + ")";
                }
                else
                {
                    throw new NotSupportedException("该对象是空对象不支持插入操作！");
                }
              
            }
            return cmd;
        }

        
        //private static ConcurrentDictionary<Type, DbCommandInfo> InsertCommandCache = new ConcurrentDictionary<Type, DbCommandInfo>();
        //private static DbCommandInfo GetInsertCommand()
        //{
        //    DbCommandInfo db;
        //    if (!InsertCommandCache.TryGetValue(typeof(T), out db))
        //    {
        //       db= CreateInsertCommandForType();
        //    }
        //    return db;
        //}
        //private static DbCommandInfo CreateInsertCommandForType( )
        //{
        //    DbCommandInfo result = new DbCommandInfo();
        //    Dictionary<string, DbParameter> map = new Dictionary<string, DbParameter>();
        //    IDataBase database = DataBaseFactory.CreateDataBase(DataBaseType.MSSQLServer);
        //    DbCommand cmd = database.CreateCommand();
        //    DbTableInfo tableInfo = CreateDbTableInfoForType(typeof(T));
        //    string tableName = tableInfo.TableName;
        //    if (string.IsNullOrEmpty(tableName))
        //    {
        //        throw new NullReferenceException("没有对象没有申明表名");
        //    }
        //    string insertCommand = string.Format("insert into {0}", tableName);
        //    int i = 0;
        //    string[] insertCondition = new string[tableInfo.Columns.Count];
        //    string[] valueCondition = new string[tableInfo.Columns.Count];
        //    foreach (DbColumnInfo colinfo in tableInfo.Columns)
        //    {
        //        insertCondition[i] = colinfo.ColumnName;
        //        valueCondition[i] = string.Format("{0}{1}", database.DbParameterPrefix, colinfo.ColumnName);
        //        DbParameter param = database.CreateParameter();
        //        param.Direction = ParameterDirection.Input;
        //        param.ParameterName = database.DbParameterPrefix + colinfo.ColumnName;
        //        param.Value = DBNull.Value;//初始化参数都是DBNull.Value
        //        cmd.Parameters.Add(param);
        //        map.Add(colinfo.PropertyName,param);
        //        i++;
        //    }
        //    cmd.CommandText = insertCommand + "(" + string.Join(",", insertCondition) + ") values (" + string.Join(",", valueCondition) + ")";
        //    result.Command = cmd;
        //    result.PropertyNameToParamter = map;
        //    return result;
        //}
        //private static void FillParameterForEntity(DbCommandInfo cmd, T entity)
        //{
        //   List<string> fields =  entity.GetChangedFields().ToList();
        //   Dictionary<string,object> entityValue=GetEntityToDictionaryConverter()(entity);
        //   foreach (KeyValuePair<string,DbParameter> kv in cmd.PropertyNameToParamter)
        //   {
        //       if (fields.Contains(kv.Key))
        //       {
        //           kv.Value.Value = entityValue[kv.Key];
        //       }
        //       else
        //       {
        //           kv.Value.Value = DBNull.Value;
        //       }
        //   }
        //}
        //生产Sql语句。没填的字段也

        //


        /// <summary>
        /// 暂时不使用
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="condition"></param>
        /// <param name="database"></param>
        private static void BuildInsertCommand(DbCommand cmd, T condition, IDataBase database)
        {
            if (condition == null)
            {
                throw new NotSupportedException("insert 操作实体不能为空！");
            }
            if (condition.EntityState != EntityState.New || condition.GetOrigianlEntityState() != EntityState.New)
            {
                throw new NotSupportedException("该对象的状态不支持insert操作！");
            }
        
            DbTableInfo tableInfo = CreateDbTableInfoForType(typeof(T));
            string tableName = tableInfo.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new NullReferenceException("没有对象没有申明表名");
            }
            string insertCommand = string.Format("insert into {0}", tableName);
            cmd.Parameters.Clear();
            // if (condition != null)  //上面做了判断了一次注释掉
            {
                Dictionary<string, object> fieldValues = condition.GetOriginalData(); //获取所有的修改获得数据
                if (fieldValues.Count > 0)
                {
                    //  Dictionary<string, object> entityValues = GetEntityToDictionaryConverter()(condition);
                    string[] valueCondition = new string[fieldValues.Count];
                    int i = 0;
                    foreach (KeyValuePair<string, object> kv in fieldValues)
                    {
                        valueCondition[i] = string.Format("{0}{1}", database.DbParameterPrefix, kv.Key);
                        DbParameter param = database.CreateParameter();
                        param.Direction = ParameterDirection.Input;
                        param.ParameterName = database.DbParameterPrefix + kv.Key;
                        if (kv.Value == null)
                        {
                            param.Value = DBNull.Value;
                        }
                        else
                        {
                            param.Value = kv.Value;
                            param.DbType = typeMap[kv.Key.GetType()];
                        }
                        cmd.Parameters.Add(param);
                        i++;
                    }
                    cmd.CommandText = insertCommand + "(" + string.Join(",", fieldValues.Keys.ToArray()) + ") values (" + string.Join(",", valueCondition) + ")";
                }
                else
                {
                    throw new NotSupportedException("该对象是空对象不支持插入操作！");
                }

            }
        }
        private static DbCommand BuildDeleteCommand(T condition, IDataBase database)
        {
            if (condition == null)
            {
                throw new NotSupportedException("Delete对象为空！");
            }
           // IDataBase database = DataBaseFactory.CreateDataBase(DataBaseType.MSSQLServer);
            DbCommand cmd = database.CreateCommand();
            DbTableInfo tableInfo = CreateDbTableInfoForType(typeof(T));
            string tableName = tableInfo.TableName;
            if (string.IsNullOrEmpty(tableName))
            {
                throw new NullReferenceException("没有对象没有申明表名");
            }
            if (tableInfo.PrimaryKey == null || tableInfo.PrimaryKey.Length == 0)
            {
                throw new NullReferenceException("该对象没有申明主键无法生产Delete命令");
            }
            if (condition.EntityState != EntityState.Deleted || condition.GetOrigianlEntityState() != EntityState.UnChange)
            {
                throw new NullReferenceException("该对象不支持Delete命令");
            }
          
          //  string whereSql = "";
            if (condition != null)
            {
                string deleteCommand = string.Format("delete from {0}", tableName);
                string[] PKs = tableInfo.PrimaryKey;
                Dictionary<string,object> fieldValues = condition.GetOriginalData(PKs);
                if (fieldValues.Count != PKs.Length)
                {
                    throw new NotSupportedException("主键发生错误！");
                }
                string[] whereCondition = new string[PKs.Length];
                for (int i = 0; i < PKs.Length; i++)
                {
                  

                    if (fieldValues[PKs[i]] == null)
                    {
                        whereCondition[i] = string.Format("{0} is null ", PKs[i]);
                    }
                    else
                    {
                        whereCondition[i] = string.Format("{0}={1}{0}", PKs[i], database.DbParameterPrefix);
                        DbParameter param = database.CreateParameter();
                        param.Direction = ParameterDirection.Input;
                        param.ParameterName = database.DbParameterPrefix + PKs[i];
                        param.Value = fieldValues[PKs[i]];
                        param.DbType = typeMap[fieldValues[PKs[i]].GetType()];
                        cmd.Parameters.Add(param);
                    }
                }
               string  whereSql = " Where " + string.Join(" AND ", whereCondition);//where 语句
               cmd.CommandText = deleteCommand + whereSql;
            }
           
            return cmd;
        }

        #endregion 生产SQL语句

        #region Public Methods

        //有则利用缓存
        internal static DbTableInfo GetDbTableInfoByType(Type t)
        {
            DbTableInfo lst;
            if (!DbColumnInfoCache.TryGetValue(t, out lst))
            {
                lst = CreateDbTableInfoForType(t);
                DbColumnInfoCache.TryAdd(t, lst);
            }
            return lst;
        }
        internal static Converter<T, Dictionary<string, object>> GetEntityToDictionaryConverter()
        {
             Converter<T, Dictionary<string, object>> convert;
             if (!EntityToDictionaryCache.TryGetValue(typeof(T),out convert))
             {
                 convert = CreateKVForEntity();
                 EntityToDictionaryCache.TryAdd(typeof(T), convert);
             }
             return convert;
        }
        internal static Converter<IDataReader, List<T>> GetEntityConverter(bool useAop)
        {
            Converter<IDataReader, List<T>> convert;
            Type t = typeof(T);
            //生产唯一键
            string identity = t.Module.Name + t.FullName + useAop.ToString();
            if (!EntityConverterCache.TryGetValue(identity, out convert))
            {
                List<DbColumnInfo> lst = GetDbTableInfoByType(t).Columns;
                convert = CreateBatchDataLoader(lst, useAop);
                EntityConverterCache.TryAdd(identity, convert);
            }
            //if (!EntityConverterCache.TryGetValue(typeof(T), out convert))
            //{
            //    List<DbColumnInfo> lst = GetDbTableInfoByType(t).Columns;
            //    convert = CreateBatchDataLoader(lst, useAop);
            //    EntityConverterCache.TryAdd(typeof(T), convert);
            //}
            return convert;
        }

        #region 查询方法
        public static List<T> GetDataByDataReader(IDataReader dr,bool useAop)
        {
            Converter<IDataReader, List<T>> converter = GetEntityConverter(useAop);
            return converter(dr) as List<T>;
        }
        //public static List<T> GetDataBySql(string SqlText, bool isProcedure, DbConnection conn)
        //{
        //    //using (SqlConnection conn = new SqlConnection(connectionString))
        //    //{
        //        try
        //        {
        //            conn.Open();
        //            SqlCommand cmd = conn.CreateCommand();
        //            cmd.CommandText = SqlText;
        //            if (isProcedure)
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //            }
        //            SqlDataReader sdr = cmd.ExecuteReader();
        //            List<T> data = GetDataByDataReader(sdr);
        //            return data;
        //        }
        //        catch (IndexOutOfRangeException iex)
        //        {
        //            throw new NullReferenceException(iex.Message + "不再查询结果中.");
        //        }
        //        catch (SqlException sex)
        //        {
        //            throw sex;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    //}
        //}
        //public static List<T> GetDataBySql(string SqlText, Dictionary<string, string> paramInfo, bool isProcedure, DbConnection conn)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        try
        //        {
        //            conn.Open();
        //            SqlCommand cmd = conn.CreateCommand();
        //            cmd.CommandText = SqlText;
        //            if (isProcedure)
        //            {
        //                cmd.CommandType = CommandType.StoredProcedure;
        //            }
        //            if (paramInfo != null && paramInfo.Keys.Count > 0)
        //            {
        //                foreach (KeyValuePair<string, string> kv in paramInfo)
        //                {
        //                   SqlParameter param=  cmd.CreateParameter();
        //                   param.ParameterName = kv.Key;
        //                   param.Value = kv.Value;
        //                   cmd.Parameters.Add(param);
        //                }
        //            }
        //            SqlDataReader sdr = cmd.ExecuteReader();
        //            List<T> data = GetDataByDataReader(sdr);
        //            return data;
        //        }
        //        catch (IndexOutOfRangeException iex)
        //        {
        //            throw new NullReferenceException(iex.Message + "不再查询结果中.");
        //        }
        //        catch (SqlException sex)
        //        {
        //            throw sex;
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //}
        public static List<T> GetAllData(DbConnection conn, bool useAop, IDataBase database)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DbCommand cmd = BuildSimpleSelectCommand(database);
                cmd.Connection = conn;
                conn.TryOpen();
                DbDataReader dr = cmd.ExecuteReader();
                List<T> data = GetDataByDataReader(dr, useAop);
                data.ForEach(t => { t.EntityState = EntityState.UnChange; t.OriginalEntityState = EntityState.UnChange; });
                return data;
            }
        }
        public static List<T> GetDataByCondition(T conditon, DbConnection conn, bool useAop,IDataBase database)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DbCommand cmd = BuildSelectCommand(conditon, database);
                cmd.Connection = conn;
                conn.TryOpen();
                DbDataReader dr = cmd.ExecuteReader();
                List<T> data = GetDataByDataReader(dr, useAop);
               // data.ForEach(t => { t.EntityState = EntityState.UnChange; t.OriginalEntityState = EntityState.UnChange; t.ClearChangedFields(); });
                return data;
            }
        }

        public static List<T> GetDataByCondition(DbSearchConditionCollection<T> conditon, DbConnection conn, bool useAop, IDataBase database)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DbCommand cmd = BuildSelectCommand(conditon, database);//select * from ......
                cmd.Connection = conn;
                conn.TryOpen();
                DbDataReader dr = cmd.ExecuteReader();
                List<T> data = GetDataByDataReader(dr, useAop);
                // data.ForEach(t => { t.EntityState = EntityState.UnChange; t.OriginalEntityState = EntityState.UnChange; t.ClearChangedFields(); });
                return data;
            }
        }

        #endregion
        public static void Update(T entity, DbConnection conn,IDataBase db)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            {
                DbCommand cmd = BuildUpdateCommand(entity,db);
                cmd.Connection = conn;
                conn.TryOpen();
                cmd.ExecuteNonQuery();
            }
        }
        public static void Update(ICollection<T> entities, DbConnection conn,IDataBase db)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            {
                foreach (T entity in entities)
                {
                    DbCommand cmd = BuildUpdateCommand(entity,db);
                    cmd.Connection = conn;
                    conn.TryOpen();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void Insert(T entity, DbConnection conn,IDataBase db)
        {
            {
                DbCommand cmd = BuildInsertCommand(entity,db);
                cmd.Connection = conn;
                conn.TryOpen();
                cmd.ExecuteNonQuery();
            }
        }
        public static void Insert(ICollection<T> entities, DbConnection conn, IDataBase db)
        {
            {
                foreach (T entity in entities)
                {
                    Insert(entity, conn, db);
                }
            }
        }
        //public static void Insert_v2(ICollection<T> entities, DbConnection conn)
        //{
        //    //using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        if (entities.Count < 20)
        //        {
        //            Insert(entities, conn);
        //        }
        //        else
        //        {
        //            T[] arr = entities.ToArray();
        //            //默认开4个线程
        //            int threadCount = 4; // 线程的数目
        //            int eachThreadCount=arr.Length/threadCount;//每个线程的数据条数
        //            bool[] isfinished = new bool[threadCount];
        //            DateTime now = new DateTime();
        //            List<Exception> lst = new List<Exception>();
        //            for (int i = 0; i < threadCount; i++)
        //            {
        //                int currentIndex = i;
        //                BackgroundWorker work = new BackgroundWorker();
        //                work.DoWork += delegate(object sender, DoWorkEventArgs e)
        //                {
        //                    int beginIndex = currentIndex * eachThreadCount;//开始编号包含
        //                    int endIndex = (currentIndex + 1) * eachThreadCount;//结束编号但不包含
        //                    if (currentIndex == threadCount)
        //                    { 
        //                         endIndex=arr.Length;
        //                    }
        //                    for (int j = beginIndex; j < endIndex; j++)
        //                    {
        //                        Insert(arr[j], conn);
        //                    }
        //                };
        //                work.RunWorkerCompleted += delegate(object sender, RunWorkerCompletedEventArgs e)
        //                {
        //                    if (e.Error != null)
        //                    {
        //                        lst.Add(e.Error);
        //                    }
        //                    isfinished[currentIndex] = true;
        //                };
        //                work.RunWorkerAsync();
        //            }
        //            while (isfinished.Contains(false))
        //            { 
                       
        //            }
                    
        //        }
               
        //    }
        //}
        public static void SqlBulkInsert(ICollection<T> entities, SqlConnection conn)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
                DbTableInfo info = CreateDbTableInfoForType(typeof(T));
                SqlBulkCopy copy = new SqlBulkCopy(conn.ConnectionString);
                var convert=   GetEntityToDictionaryConverter();
                copy.DestinationTableName =info.TableName;
                DataTable dt=new DataTable ();
                foreach(DbColumnInfo col in info.Columns )
                {
                    dt.Columns.Add(col.PropertyName);
                    copy.ColumnMappings.Add(col.PropertyName, col.ColumnName);
                }
                foreach (T entity in entities)
                {
                   DataRow dr = dt.NewRow();
                   Dictionary<string,object> values = convert(entity);
                   foreach (DataColumn col in dt.Columns)
                   {
                       dr[col.ColumnName] = values[col.ColumnName];
                   }
                   dt.Rows.Add(dr);
                }
                conn.TryOpen();
                copy.WriteToServer(dt);
            //}
        }
        public static void Delete(T entity, DbConnection conn,IDataBase db)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
                DbCommand cmd = BuildDeleteCommand(entity,db);
                cmd.Connection = conn;
                conn.TryOpen();
                cmd.ExecuteNonQuery();
            //}
        }
        public static void Delete(ICollection<T> entites, DbConnection conn, IDataBase db)
        {
            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
                foreach (T entity in entites)
                {
                    DbCommand cmd = BuildDeleteCommand(entity,db);
                    cmd.Connection = conn;
                    conn.TryOpen();
                    cmd.ExecuteNonQuery();
                }
            //}
        }

        #endregion
    }

    public enum DataBaseType
    { 
      MSSQLServer,
      Oracle10Plus,
      Oracle9i
    }
    public enum WhereConditionOperation
    {
        Append, //直接附加在后面
        Wrap    //做一层封装
    }


}
