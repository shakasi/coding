using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Shaka.DAL;
using Shaka.Domain;

namespace Shaka.BLL
{
    public class HQIconOperateBLL
    {
        private HQIconOperateDAL _dal = new HQIconOperateDAL();

        public List<StoreGroupInfo> GetStoreGroupByUser(string loginID, int pageIndex, string sortExpression, int pageSize, ref int totalRow)
        {
            return _dal.GetStoreGroupByUser(loginID, pageIndex, sortExpression, pageSize, ref totalRow);
        }

        public List<HQIconInfo> GetIcon(string selectedFields, string TVName, string whereStatement,
            string sortExpression, int pageIndex, int pageSize, string sgType, string sgNumbers, string effectiveDate, ref int totalRow)
        {
            return _dal.GetIcon(selectedFields, TVName, whereStatement, sortExpression, pageIndex, pageSize, sgType, 
                sgNumbers, effectiveDate, ref totalRow);
        }

        public ResultInfo SaveIcon(List<HQSadDTICONInfo> sadDTIconList)
        {
            return _dal.SaveIcon(sadDTIconList);
        }
    }
}