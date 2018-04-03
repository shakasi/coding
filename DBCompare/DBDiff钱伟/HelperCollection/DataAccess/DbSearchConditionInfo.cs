using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.HelperCollection.DataAccess
{
    public class DbSearchConditionInfo
    {
       public string Field { get; set; }
       public DbSearchOperation SearchOperation { get; set; }
       public Type FieldDataType { get; set; }
       public object FieldData { get; set; }
    }
    public enum DbSearchOperation
    {
        equals,
        does_not_equal,
        is_greater_than,
        is_greater_than_or_equal_to,
        is_less_than,
        is_less_than_or_equal_to,
        blanks,
        non_blanks,
        like,
        not_like,
    }
}
