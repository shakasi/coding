using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.AOP
{
    public class MethodContext
    {
        /// <summary>
        /// 
        /// </summary>
        public object Executor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MethodName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Processed { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public object[] Parameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; set; }
    }
}
