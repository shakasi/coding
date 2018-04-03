using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Shaka.Utils
{
    public class ComparerHelper<T> : IEqualityComparer<T> where T : new()
    {
        private PropertyInfo[] _proArray;
        public ComparerHelper(params string[] proNameArray)
        {
            if (proNameArray == null || proNameArray.Length == 0)
            {
                _proArray = typeof(T).GetProperties();
            }
            else
            {
                List<PropertyInfo> proList = new List<PropertyInfo>();
                foreach (string proName in proNameArray)
                {
                    proList.Add(typeof(T).GetProperty(proName));
                }
                _proArray = proList.ToArray() ;
            }
        }

        public bool Equals(T x, T y)
        {
            foreach (PropertyInfo pro in _proArray)
            {
                if (!pro.GetValue(x, null).Equals(pro.GetValue(y, null)))
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(T obj)
        {
            int result = 0;
            foreach (PropertyInfo pro in _proArray)
            {
                result ^= pro.GetValue(obj, null).GetHashCode();
            }
            return result;
        }
    }
}
