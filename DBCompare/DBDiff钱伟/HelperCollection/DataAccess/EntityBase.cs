using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.AOP;
using Utility.HelperCollection.DataAccess.CallHandler;

namespace Utility.HelperCollection.DataAccess
{
    [Serializable]
    public abstract class EntityBase
    {
        //to do 增加一些common方法
        //当前的entity 状态（可以为任何一个）     与_OriginalEntityState做个双保险防止数据被篡改（自定义的callhandler可以肆意的修改_EntityState，但是不能通过callhandler来修改_OriginalEntityState）
        private EntityState _EntityState;
        //原始的entity 状态（只能为New或者Unchanged）  New是新创建的  Unchanged是通过DataReader来创建的
        private EntityState _OriginalEntityState;
        internal EntityState OriginalEntityState
        {
            get { return _OriginalEntityState; }
            set { _OriginalEntityState = value; }
        }
        //原始数据(如果 _OriginalEntityState是New则没什么用如果是 Unchanged则存的是初始化后的数据)
        internal Dictionary<string, object> _OriginalData{get;set;}
       // private readonly Dictionary<string, object> _CurrentData;
        //修改的字段
        private List<string> _ChangedFields;
        public EntityState GetOrigianlEntityState()
        {
            return _OriginalEntityState;
        }
        //返回修改字段的副本
        public Dictionary<string,object> GetOriginalData(string[] fields)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            foreach (string s in fields)
            {
                if (_OriginalData.ContainsKey(s))
                {
                    result.Add(s, _OriginalData[s]);
                }
            }
            return result;
        }
        public Dictionary<string, object> GetOriginalData()
        {
           return GetOriginalData(_OriginalData.Keys.ToArray());
        }
        public string[] GetChangedFields()
        { 
            string[] arr=new string[_ChangedFields.Count];
             _ChangedFields.CopyTo(arr);
             return arr;
        }
        public void ClearChangedFields()
        {
            _ChangedFields.Clear();
        }
        internal void AddChangedField(string fieldName,object fieldValue)
        {
            if (this._EntityState == EntityState.Deleted)
            {
                throw new NotSupportedException("删除状态的对象不能做操作！");
            }
            if (_OriginalEntityState == EntityState.UnChange)   //说明是从datareader里面读取出来后修改的
            {
                if (_OriginalData.ContainsKey(fieldName))
                {
                    //如果相同则将该字段移除 ，不相同添加进ChangedFields
                    if (_OriginalData[fieldName]!=null&&_OriginalData[fieldName].Equals(fieldValue))
                    {
                        _ChangedFields.Remove(fieldName);
                        if (_ChangedFields.Count == 0)
                        {
                            _EntityState = EntityState.UnChange;
                        }
                    }
                    else
                    {
                        _EntityState = EntityState.Modified;
                        if (!_ChangedFields.Contains(fieldName))
                        {
                            _ChangedFields.Add(fieldName);
                        }
                    }
                }
            }
            else if (_OriginalEntityState == EntityState.New)//只有New的时候才会去修改_OriginalData
            {
                if (_OriginalData.ContainsKey(fieldName))
                {
                    _OriginalData[fieldName] = fieldValue;
                }
                else
                {
                    _OriginalData.Add(fieldName, fieldValue);
                }
            }
            else
            {
                throw new NotSupportedException(_OriginalEntityState + " 是未知的EntityState");
            
            }
        }
        //只读，不行修改(必需public)
        public EntityState EntityState
        {
            get
            {
                return _EntityState;
            }
            internal set
            {
                _EntityState = value;
                //StackTrace trace = new StackTrace();
                //MethodBase methodName = trace.GetFrame(1).GetMethod();//0.自己 1.调用者
                ////只有可信任的对象才能修改 
                //if (methodName.DeclaringType == typeof(EntityBase))// || methodName.DeclaringType == typeof(EntityValueChangeCallHandler) )
                //{
                //     _EntityState = value;
                //}
                //else if (methodName.DeclaringType == null) //生产的代码
                //{
                //    ParameterInfo[] paraminfo = trace.GetFrame(2).GetMethod().GetParameters();
                //    if (paraminfo.Length > 0 && paraminfo[0].ParameterType == typeof(IDataReader))
                //    {
                //        _EntityState = EntityState.UnChange;
                //        _OriginalEntityState = EntityState.UnChange;
                //    }
                //}
                //else if (methodName.DeclaringType.IsGenericType && methodName.DeclaringType.FullName == "Utility.HelperCollection.DataAccess.EntityCollection`1")
                //{
                //    _EntityState = value;
                //    _OriginalEntityState=value;
                //}
                //else
                //{
                //    throw new Exception("不允许修改EntityState");
                //}
       
            }
        }
        internal void Delete()
        {
            if (_OriginalEntityState == EntityState.UnChange)
            {
                this._EntityState = EntityState.Deleted;
            }

        }
        public EntityBase()
        {
            _EntityState = EntityState.New;
            _OriginalEntityState = EntityState.New;
            _OriginalData = new Dictionary<string, object>();
            _ChangedFields = new List<string>();
        }
    }

    /// <summary>
    /// 用来表示实体的状态
    /// </summary>
    public enum EntityState
    {
       New,  //到时做插入
       Modified,//到时做update
       Deleted,//做deleted
       UnChange//原始状态
    }
}
