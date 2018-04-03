using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtendFuction
{
    public static class IntExtend
    {
        public static bool Between(this int a, int first, int second)
        {
            if (a == first)
            {
                return true;
            }
            else if (a == second)
            {
                return true;
            }
            else if (a - first > 0 ^ a - second > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }
    }
}
