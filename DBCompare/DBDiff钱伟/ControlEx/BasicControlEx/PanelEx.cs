using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using ExtendFuction;

namespace ControlEx.BasicControlEx
{
     public class PanelEx:Panel
    {
        public PanelEx()
        {
 
           
        }
        #region 字段
        List<Splitter> lst = new List<Splitter>();
        private Point enterPoint;
        DateTime lastMoveTime = new DateTime();
        #endregion

        #region 属性字段
        private bool _allowExtendLeft;
        private bool _allowExtendRight;
        private bool _allowExtendTop;
        private bool _allowExtendBottom;
        private int _splitterWidth = 3;

        private int _minWidth = 1;
        private int _minHeight = 1;
        private string _id;
        #endregion

        #region 自定义属性
        [Description("向左延伸")]
        public bool AllowExtendLeft
        {
            get
            {
                return _allowExtendLeft;
            }
            set
            {
                _allowExtendLeft = value;
                if (value)
                {
                    AddSplitter(DockStyle.Left);
                }
            }
        }
        [Description("向右延伸")]
        public bool AllowExtendRight
        {
            get
            {
                return _allowExtendRight;
            }
            set
            {
                _allowExtendRight = value;
                if (value)
                {
                    AddSplitter(DockStyle.Right);
                }
            }
        }
        [Description("向上延伸")]
        public bool AllowExtendTop
        {
            get
            {
                return _allowExtendTop;
            }
            set
            {
                _allowExtendTop = value;
                if (value)
                {
                    AddSplitter(DockStyle.Top);
                }
            }
        }
        [Description("向下延伸")]
        public bool AllowExtendBottom
        {
            get { return _allowExtendBottom; }
            set
            {
                _allowExtendBottom = value;
                if (value)
                {
                    AddSplitter(DockStyle.Bottom);
                }
            }
        }
        [Description("选择区域的宽度")]
        [DefaultValue(3)]
        public int SplitterWidth
        {
            get
            {
                return _splitterWidth;
            }
            set
            {
                if (value > 0)
                {
                    _splitterWidth = value;
                    foreach (Splitter sp in lst)
                    {
                        switch (sp.Dock)
                        {
                            case DockStyle.Left:
                            case DockStyle.Right: sp.Width = value; break;
                            case DockStyle.Top:
                            case DockStyle.Bottom: sp.Height = value; break;
                        }
                    }
                }
                else
                {
                    throw new Exception("必须大于0");
                }
            }
        }

        [Description("最小的宽")]
        [DefaultValue(1)]
        public int MinWidth
        {
            get
            {
                return _minWidth;
            }
            set
            {
                if (value > 0)
                {
                    _minWidth = value;
                }
                else
                {
                    throw new NotSupportedException("最小长度必须大于0");
                }
            }
        }
        [Description("最小的长")]
        [DefaultValue(1)]
        public int MinHeight
        {
            get
            {
                return _minHeight;
            }
            set
            {
                if (value > 0)
                {
                    _minHeight = value;
                }
                else
                {
                    throw new NotSupportedException("最小长度必须大于0");
                }
            }

        }

        [Browsable(false)]
        [Description("控件的ID（需要保证唯一性）")]
        [DefaultValue(1)]
        public string ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        #endregion

        #region 方法
        private void AddSplitter(DockStyle ds)
        {
        
            Splitter sl = new Splitter();
            sl.Dock = ds;
            this.Controls.Add(sl);
            sl.BringToFront();
            lst.Add(sl);
            sl.MouseDown += (object send, MouseEventArgs e) =>
                {
                    sl.Tag = "True";
                    enterPoint = Control.MousePosition;
                };
            sl.MouseMove += (object send, MouseEventArgs e) =>
                {
                    if (sl.Tag != null && sl.Tag.ToString() == "True")
                    {
                        {
                            Point p = Control.MousePosition;
                            #region  下面代码可能可以用更好的方式进行实现
                            switch (ds)
                            {
                                case DockStyle.Left:
                                    if (sl.Cursor == System.Windows.Forms.Cursors.SizeWE)
                                    {
                                        MoveLeft(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNWSE)//左上角
                                    {
                                        MoveLeftTop(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNESW)//左下角
                                    {
                                        MoveLeftBottom(p);
                                    }
                                    break;
                                case DockStyle.Right:
                                    if (sl.Cursor == System.Windows.Forms.Cursors.SizeWE)
                                    {
                                        MoveRight(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNESW)//右上角
                                    {
                                        MoveRightTop(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNWSE)//右下角
                                    {
                                        MoveRightBottom(p);
                                    }
                                    break; ;
                                case DockStyle.Top: 
                                    if (sl.Cursor == System.Windows.Forms.Cursors.SizeNS) //上
                                    {
                                        MoveTop(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNWSE)//左上角
                                    {
                                        MoveLeftTop(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNESW)//右上角
                                    {
                                        MoveRightTop(p);
                                    }
                                    break;
                                case DockStyle.Bottom:
                                    if (sl.Cursor == System.Windows.Forms.Cursors.SizeNS)
                                    {
                                        MoveBottom(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNWSE)//右下角
                                    {
                                        MoveRightBottom(p);
                                    }
                                    else if (sl.Cursor == System.Windows.Forms.Cursors.SizeNESW)//左下角
                                    {
                                        MoveLeftBottom(p);
                                    }
                                    break;

                            }
                            #endregion
                         
                           lastMoveTime = DateTime.Now;//记录上次移动的时间  这个为了防止经常刷新页面导致的 闪动  后期可以考虑重写onpaint方法
                        }
                    }
                    else
                    {

                        //相对于panel 的鼠标位置
                        Point mouseToPanel = this.PointToClient(Control.MousePosition);

                        //必须DockStyle.None才允许脚上被选中
                        if (this.Dock == DockStyle.None)
                        {
                            //左上角和右下角
                            if (((mouseToPanel.X.Between(0, SplitterWidth) && mouseToPanel.Y.Between(0, SplitterWidth) && AllowExtendTop && AllowExtendLeft) || (mouseToPanel.X.Between(this.Width - SplitterWidth, this.Width) && mouseToPanel.Y.Between(this.Height - SplitterWidth, this.Height) && AllowExtendBottom && AllowExtendRight)))
                            {
                                sl.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
                            }
                            //左下角和右上角
                            else if (((mouseToPanel.X.Between(0, SplitterWidth) && mouseToPanel.Y.Between(this.Height - SplitterWidth, this.Height) && AllowExtendBottom && AllowExtendLeft) || (mouseToPanel.X.Between(this.Width - SplitterWidth, this.Width) && mouseToPanel.Y.Between(0, SplitterWidth) && AllowExtendTop && AllowExtendRight)))
                            {
                                sl.Cursor = System.Windows.Forms.Cursors.SizeNESW;
                            }
                            else
                            {
                                switch (ds)
                                {
                                    case DockStyle.Left:
                                    case DockStyle.Right: sl.Cursor = System.Windows.Forms.Cursors.SizeWE; break;
                                    case DockStyle.Top:
                                    case DockStyle.Bottom: sl.Cursor = System.Windows.Forms.Cursors.SizeNS; break;
                                }
                            }
                        }
                        else if (this.Dock == DockStyle.Left && ds == DockStyle.Right)
                        {
                            sl.Cursor = System.Windows.Forms.Cursors.SizeWE;
                        }
                        else if (this.Dock == DockStyle.Right && ds == DockStyle.Left)
                        {
                            sl.Cursor = System.Windows.Forms.Cursors.SizeWE;
                        }
                        else if (this.Dock == DockStyle.Top && ds == DockStyle.Bottom)
                        {
                            sl.Cursor = System.Windows.Forms.Cursors.SizeNS;
                        }
                        else if (this.Dock == DockStyle.Bottom && ds == DockStyle.Top)
                        {
                            sl.Cursor = System.Windows.Forms.Cursors.SizeNS;
                        }
                        else
                        {
                            sl.Cursor = System.Windows.Forms.Cursors.Default;
                        }
                    }
                };
            sl.MouseUp += (object send, MouseEventArgs e) =>
                {
                    sl.Tag = "False";   
                };
        }


        #region 移动的具体实现方法
        /// <summary>
        /// 向左移动
        /// </summary>
        /// <param name="currentPointToScreen">当前坐标</param>
        /// <returns>返回实际的坐标</returns>
        private void MoveLeft(Point currentPointToScreen)
        {
          BeginPaint();
          int x = SetControlPropertyForMoveLeft(currentPointToScreen);
          enterPoint = new Point(x, currentPointToScreen.Y);
          EndPaint();
        }

        private void MoveRight(Point currentPointToScreen)
        {
            BeginPaint();
            int x = SetControlPropertyForMoveRight(currentPointToScreen);
            enterPoint = new Point(x, currentPointToScreen.Y);
            EndPaint();
        }

        private void MoveTop(Point currentPointToScreen)
        {
            BeginPaint();
            int y = SetControlPropertyForMoveTop(currentPointToScreen);
            enterPoint = new Point(currentPointToScreen.X, y);
            EndPaint();
        }

        private void MoveBottom(Point currentPointToScreen)
        {
            BeginPaint();
            int y = SetControlPropertyForMoveBottom(currentPointToScreen);
            enterPoint = new Point(currentPointToScreen.X, y);
            EndPaint();
        }


        private void MoveLeftTop(Point currentPointToScreen)
        {
            BeginPaint();
            int x = SetControlPropertyForMoveLeft(currentPointToScreen);
            int y = SetControlPropertyForMoveTop(currentPointToScreen);
            enterPoint = new Point(x, y);
            EndPaint();
        }

        private void MoveLeftBottom(Point currentPointToScreen)
        {
            BeginPaint();
            int x = SetControlPropertyForMoveLeft(currentPointToScreen);
            int y = SetControlPropertyForMoveBottom(currentPointToScreen);
            enterPoint = new Point(x, y);
            EndPaint();
        }

        private void MoveRightTop(Point currentPointToScreen)
        {
            BeginPaint();
            int x = SetControlPropertyForMoveRight(currentPointToScreen);
            int y = SetControlPropertyForMoveTop(currentPointToScreen);
            enterPoint = new Point(x, y);
            EndPaint();
        }

        private void MoveRightBottom(Point currentPointToScreen)
        {
            BeginPaint();
            int x = SetControlPropertyForMoveRight(currentPointToScreen);
            int y = SetControlPropertyForMoveBottom(currentPointToScreen);
            enterPoint = new Point(x, y);
            EndPaint();
        }

        private int SetControlPropertyForMoveLeft(Point currentPointToScreen)
        {
            int minWidthForControl = MinWidth + 2 * SplitterWidth;
            if (this.Width + (enterPoint.X - currentPointToScreen.X) >= minWidthForControl)
            {
                this.Location = new System.Drawing.Point(this.Location.X + (currentPointToScreen.X - enterPoint.X), this.Location.Y);
                this.Width = this.Width + (enterPoint.X - currentPointToScreen.X);
                // enterPoint = currentPointToScreen;             //记录上一次的坐标（）相对于显示器的 
                return currentPointToScreen.X;
            }
            else
            {
                //当小于最小宽的时候 就设置为宽 
                //但此时的enterpoint该怎么设置呢？设置为边缘？
                this.Location = new System.Drawing.Point(this.Location.X + (this.Width - minWidthForControl), this.Location.Y);
                this.Width = minWidthForControl;
                Point locationRelatedToScreen = this.PointToScreen(Point.Empty);//这个没问题获取这个控件的0,0点对应于界面的坐标
                //enterPoint = new Point(locationRelatedToScreen.X + SplitterWidth / 2, currentPointToScreen.Y); //定位到1/2的splitter
                return locationRelatedToScreen.X + SplitterWidth / 2;
            }
        }
        private int SetControlPropertyForMoveRight(Point currentPointToScreen)
        {
            int minWidthForControl = MinWidth + 2 * SplitterWidth;
            if (this.Width + (currentPointToScreen.X - enterPoint.X) >= minWidthForControl)
            {
                this.Width = this.Width + (currentPointToScreen.X - enterPoint.X);
                //enterPoint = currentPointToScreen;             //记录上一次的坐标（）相对于显示器的
                return currentPointToScreen.X;
            }
            else
            {
                this.Width = minWidthForControl;
                Point locationRelatedToScreen = this.PointToScreen(Point.Empty);//这个没问题获取这个控件的0,0点对应于界面的坐标
                // enterPoint = new Point(locationRelatedToScreen.X+ this.Width - SplitterWidth / 2, currentPointToScreen.Y); 
                return locationRelatedToScreen.X + this.Width - SplitterWidth / 2;
            }
        }
        private int SetControlPropertyForMoveTop(Point currentPointToScreen)
        {
            int minHeightForControl = MinHeight + 2 * SplitterWidth;
            if (this.Height + (enterPoint.Y - currentPointToScreen.Y) >= minHeightForControl)
            {
                this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + (currentPointToScreen.Y - enterPoint.Y));
                this.Height = this.Height + (enterPoint.Y - currentPointToScreen.Y);
                // enterPoint =currentPointToScreen;             //记录上一次的坐标（）相对于显示器的
                return currentPointToScreen.Y;
            }
            else
            {
                this.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + (this.Height - minHeightForControl));
                this.Height = minHeightForControl;
                Point locationRelatedToScreen = this.PointToScreen(Point.Empty);//这个没问题获取这个控件的0,0点对应于界面的坐标
                // enterPoint = new Point(currentPointToScreen.X, locationRelatedToScreen.Y+SplitterWidth/2); //定位到1/2的splitter
                return locationRelatedToScreen.Y + SplitterWidth / 2;
            }

        }
        private int SetControlPropertyForMoveBottom(Point currentPointToScreen)
        {
            int minHeightForControl = MinHeight + 2 * SplitterWidth;
            if (this.Height + (currentPointToScreen.Y - enterPoint.Y) >= minHeightForControl)
            {
                this.Height = this.Height + (currentPointToScreen.Y - enterPoint.Y);
                // enterPoint = currentPointToScreen;             //记录上一次的坐标（）相对于显示器的
                return currentPointToScreen.Y;
            }
            else
            {
                this.Height = minHeightForControl;
                Point locationRelatedToScreen = this.PointToScreen(Point.Empty);//这个没问题获取这个控件的0,0点对应于界面的坐标
                //enterPoint = new Point(currentPointToScreen.X, locationRelatedToScreen.Y +this.Height - SplitterWidth / 2); 
                return locationRelatedToScreen.Y + this.Height - SplitterWidth / 2;
            }
        }

        #endregion


        #endregion 方法


        #region 系统API
        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;
        private void BeginPaint()
        {
           SendMessage(this.Handle, WM_SETREDRAW, 0, IntPtr.Zero);
        }
        private void EndPaint()
        {
            SendMessage(this.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
            this.Parent.Refresh();//让父容器刷新//以后可以修改为设置父容器的相关区域无效然后刷新无效区域 
        }
        #endregion

    }

}
