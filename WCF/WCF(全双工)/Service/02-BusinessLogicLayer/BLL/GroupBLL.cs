using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cuscapi.Model;
using Cuscapi.DALFactory;
using Cuscapi.IDAL;

namespace Cuscapi.BLL
{
    /// <summary>
    /// Group（BLL）
    /// </summary>
    public class GroupBLL
    {
        private IGroup _groupDAL = Cuscapi.DALFactory.Factory.GetGroupDAL();

        public List<Cuscapi.Model.GroupInfo> AllGroup()
        {
            return _groupDAL.AllGroup();
        }
    }
}
