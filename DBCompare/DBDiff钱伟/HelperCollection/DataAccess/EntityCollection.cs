using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.AOP;

namespace Utility.HelperCollection.DataAccess
{
    //对象的集合返回值
    public class EntityCollection<T>:ICollection<T>  where T : EntityBase,new()
    {

        public EntityCollection(DBHelperBase<T> dataBase)
        {
            this.DataBase = dataBase;
        }

        List<T> data = new List<T>();

        #region ICollection方法
     
        public void Add(T item)
        {
            data.Add(item);
        }

        public void Clear()
        {
            data.Clear();
        }

        public bool Contains(T item)
        {
            return data.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            data.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return data.Where(t => t.EntityState != EntityState.Deleted).Count(); }
        }

        public bool IsReadOnly
        {
            get;
            internal set;
        }

        public bool Remove(T item)
        {
            return data.Remove(item);
        }

        /// <summary>
        /// 获取不是deleted状态的数据集合
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            var query = from entity in data where entity.EntityState!=EntityState.Deleted select entity;
            return  query.ToList().GetEnumerator();
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            var query = from entity in data where entity.EntityState != EntityState.Deleted select entity;
            return query.ToList().GetEnumerator();
        }
        #endregion

        #region 扩展方法
        /// <summary>
        /// 所有集合的数量
        /// </summary>
        public int TotalCount
        {
            get { return data.Count; }
        }

        public void Add(ICollection<T> entities)
        {
            foreach (T entity in entities)
            {
               // if (entity.GetType().BaseType == typeof(T))
               // {
                    data.Add(entity);
               // }
            }
        }
        /// <summary>
        /// 获取特定状态的数据集合
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IEnumerable<T> GetEntitiesByEntityState(params EntityState[] state)
        {
            var query = from entity in data where state.Contains(entity.EntityState) select entity;
            return query;
        }
        /// <summary>
        /// 获取所有的集合
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllEntities()
        {
            return data.AsEnumerable();
        }
        public DBHelperBase<T> DataBase { get; private set; }
        public void Delete(T item)
        {
            if (item.EntityState == EntityState.New)
            {
                Remove(item);
            }
            else
            {
                item.Delete();
            }
        }
        public void Delete(ICollection<T> items)
        {
            foreach (T entity in items)
            {
                Delete(entity);
            }
        }
        public void Update()
        {
            if (!IsReadOnly)
            {
                DataBase.Insert(GetEntitiesByEntityState(EntityState.New).ToList());
                DataBase.Update(GetEntitiesByEntityState(EntityState.Modified).ToList());
                DataBase.Delete(GetEntitiesByEntityState(EntityState.Deleted).ToList());
                AcceptChanges();
            }
            else
            {
                throw new NotSupportedException("该对象是只读的不能调用update方法！");
            }
        }
        public void AcceptChanges()
        {
            Predicate<T> match = new Predicate<T>(delegate(T entity) { return entity.EntityState == EntityState.Deleted; });
            data.RemoveAll(match);
            foreach (T entity in this.GetEntitiesByEntityState(EntityState.Modified,EntityState.New))
            {
                entity.EntityState = EntityState.UnChange;
                entity.OriginalEntityState = EntityState.UnChange;
                entity._OriginalData = SqlMap<T>.GetEntityToDictionaryConverter()(entity);
                entity.ClearChangedFields();
            }
        }
        public T this[int index]
        {
            get { return data[index]; }
           //set { data[index] = value; }
        }
        public T CreateNewEntity()
        {
            return InstanceBuilder.CreateInstance<T>();
        }
        #endregion



    }
}
