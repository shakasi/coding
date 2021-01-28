using System;
using System.Collections.Generic;
using System.Text;

namespace VNext.Entity
{
    /// <summary>
    /// 指定表名
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
    public class TableAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">数据库表名</param>
        public TableAttribute(string name = null)
        {
            if (name != null) this.Name = name;
        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 数据库模式
        /// </summary>
        public string Schema { get; set; }
    }
}
