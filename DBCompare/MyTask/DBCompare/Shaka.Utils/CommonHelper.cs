using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Shaka.Model;
using Shaka.Model.Attr;

namespace Shaka.Utils
{
    public class CommonHelper
    {
        public static string GetAttrProValue(PropertyInfo pro,string attrProName)
        {
            object[] attrArray = pro.GetCustomAttributes(false);
            if (attrArray == null || attrArray.Length == 0)
            {
                return "";
            }

            SqlColumnAttribute attr = attrArray.First() as SqlColumnAttribute;
            object attrProValue = attr.GetType().GetProperty(attrProName).GetValue(attr, null)??"";
            return attrProValue.ToString();
        }

        public static string CompareEntity(TableInfo t1, TableInfo t2)
        {
            StringBuilder diffMsgSB = new StringBuilder();
            PropertyInfo[] proArray = t1.GetType().GetProperties();
            foreach (PropertyInfo pro in proArray)
            {
                //DiffReason为空则不比较差异
                string diffReason = CommonHelper.GetAttrProValue(pro, "DiffReason");
                if (diffReason=="")
                {
                    continue;
                }
                //DiffReason不为空比较差异
                if (!pro.GetValue(t1, null).Equals(t2.GetType().GetProperty(pro.Name).GetValue(t2, null)))
                {
                    diffMsgSB.Append("\r\n  " + diffReason);
                }
            }
            return diffMsgSB.ToString().TrimStart("\r\n  ".ToArray());
        }
    }
}
