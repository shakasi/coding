using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlEx.BasicControlEx
{
    public class LoadingControl : Control
    {
        Image image =null;
        public LoadingControl()
        {
            image = ControlEx.Properties.Resources.Loading;

            SetStyle(ControlStyles.UserPaint, true);

            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;

        }

        bool isRuning = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (image != null)
            {
                if (!isRuning && ImageAnimator.CanAnimate(image))
                {
                    ImageAnimator.Animate(image, (object sender, EventArgs e111) => { this.Invalidate(); });
                    isRuning = true;
                }
               ImageAnimator.UpdateFrames(image); 
               e.Graphics.DrawImage(image, new Point(((this.Width - image.Width) / 2), (this.Height - image.Height) / 2)); 
            }
        }
        

    }
}
