using ControlEx.CommonForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlEx.Entity
{
    public class DockControlEntity
    {
        //图标
        public Image Icon { get; set; }
        //菜单文字
        public string MenuText { get; set; }

        public AddOneBaseForm DisplayForm { get; set; }

        public bool IsAutoHide { get; set; }
    }

}
