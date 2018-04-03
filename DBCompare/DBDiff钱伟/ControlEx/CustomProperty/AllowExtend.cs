using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlEx.CustomProperty
{
    [TypeConverter()]
    public class AllowExtend
    {
        public bool A
        {
            get;
            set;
        }
        public bool B
        {
            get;
            set;
        }
    }
    public class AllowExtendConverter : TypeConverter
    { 
    
    }
}
