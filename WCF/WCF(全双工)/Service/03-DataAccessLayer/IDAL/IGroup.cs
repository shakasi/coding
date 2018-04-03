using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cuscapi.Model;

namespace Cuscapi.IDAL
{
    /// <summary>
    /// Group接口
    /// </summary>
    public interface IGroup
    {
       List<GroupInfo> AllGroup();
    }
}
