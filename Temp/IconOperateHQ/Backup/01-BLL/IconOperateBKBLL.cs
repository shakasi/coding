using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Shaka.DAL;
using Shaka.Domain;

namespace Shaka.BLL
{
    public class IconOperateBLL
    {
        private IconOperateBKDAL _dal = new IconOperateBKDAL();

        public List<IconBKInfo> QueryIcon(int iconNumber, string iconName)
        {
            return _dal.QueryIcon(iconNumber, iconName);
        }

        public ResultInfo SaveIcon(int iconNumber, string iconName, FileInfo file)
        {
            return _dal.SaveIcon(iconNumber, iconName, file);
        }
    }
}
