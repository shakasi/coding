using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExtendFuction
{
    public static  class ControlExtend
    {
        public static void MouseLeaveExtend(this Control ctl, Action function)
        {
            MouseLeaveExtend(ctl, function, ctl);
        }
        private static void MouseLeaveExtend(this Control ctl, Action function, Control baseControl)
        {
            ctl.MouseLeave += (object sender, EventArgs arg) =>
            {
                Point p = Control.MousePosition;
                Rectangle r = new Rectangle(baseControl.PointToScreen(Point.Empty), baseControl.Size);
                if (!r.Contains(p))
                {
                    function();
                }
            };
            foreach (Control c in ctl.Controls)
            {
                c.MouseLeaveExtend(function, baseControl);
            }
        }
    }
}
