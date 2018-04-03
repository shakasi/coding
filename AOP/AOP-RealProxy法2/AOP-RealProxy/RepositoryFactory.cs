using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AOP_RealProxy
{
    public class RepositoryFactory
    {
        public static IRepository<T> Create<T>()
        {
            var repository = new Repository<T>();
            var dynamicProxy = new DynamicProxy<IRepository<T>>(repository);
            return dynamicProxy.GetTransparentProxy() as IRepository<T>;
        }
    }
}
