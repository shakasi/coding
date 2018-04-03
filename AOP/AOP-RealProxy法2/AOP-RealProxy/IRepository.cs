using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOP_RealProxy
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
    }
}
