using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Shaka.DAL;
using Shaka.Domain;
using Shaka.Infrastructure;
using log4net;

namespace Shaka.BLL
{
    public class IconOperateBKBLL
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IconOperateBKDAL _dal = new IconOperateBKDAL();

        public List<IconBKInfo> QueryIcon(int pageIndex,int pageSize, ref int totalRow)
        {
            List<IconBKInfo> iconList = null;
            string sqlStr = @"select * from sadDTICON";
            string orderStr= @"order by updt desc";
            SearchParamInfo parInfo = new SearchParamInfo(null, sqlStr, null, orderStr, pageIndex, pageSize);

            int pageCount = 0;
            try
            {
                iconList= _dal.GetItems<IconBKInfo>(ref pageCount, ref totalRow, parInfo.GetPars());
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
            }
            return iconList;
        }

        public ResultInfo SaveIcon(List<FileInfo> fileList)
        {
            List<IconBKInfo> iconList = new List<IconBKInfo>();
            StringBuilder upProDTProDefSB = new StringBuilder();
            foreach (FileInfo file in fileList)
            {
                IconBKInfo sadDTIcon = new IconBKInfo();

                //1001_小吃A.jpg 取 小吃A
                int IconNameBegin = file.Name.IndexOf('_') + 1;
                int IconNameEnd = file.Name.LastIndexOf('.');
                sadDTIcon.IconName = file.Name.Substring(IconNameBegin, IconNameEnd - IconNameBegin);
                string iconNumber = file.Name.Substring(0, file.Name.LastIndexOf('_'));
                int iconNumberTail = 0;
                string upProDTProDefStr = string.Format("update proDTProDef set IconNumber={0} where ProDefNumber={1}", iconNumber+ iconNumberTail, iconNumber);
                if (sadDTIcon.IconName.EndsWith("detail", StringComparison.InvariantCultureIgnoreCase))
                {
                    iconNumberTail = 1;
                    upProDTProDefStr = string.Format("update proDTProDef set detailIconNumber={0} where ProDefNumber={1}", iconNumber + iconNumberTail, iconNumber);
                }
                else if (sadDTIcon.IconName.EndsWith("display", StringComparison.InvariantCultureIgnoreCase))
                {
                    iconNumberTail = 2;
                    upProDTProDefStr = string.Format("update proDTProDef set displayIcon={0} where ProDefNumber={1}", iconNumber + iconNumberTail, iconNumber);
                }
                upProDTProDefSB.Append(upProDTProDefStr+"\r\n");

                sadDTIcon.IconNumber = Convert.ToInt32(iconNumber + iconNumberTail);
                sadDTIcon.IconType = 1;
                if (file.Extension.Equals(".gif", StringComparison.InvariantCultureIgnoreCase))
                {
                    sadDTIcon.IconType = 2;
                }
                sadDTIcon.IconImage = BinaryHelper.FileToBytes(file);
                sadDTIcon.Status = true;
                sadDTIcon.UpUser = 1;
                sadDTIcon.UpDT = DateTime.Now;
                iconList.Add(sadDTIcon);
            }
            try
            {
                _dal.SaveIcon(iconList, upProDTProDefSB.ToString());
            }
            catch (Exception ex)
            {
                _log.Fatal(ex);
                return ResultInfo.Fail;
            }
            return ResultInfo.Success;
        }
    }
}
