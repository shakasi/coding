using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ControlEx.BasicControlEx;
using System.Drawing;
using System.ComponentModel;
using System.Data;

namespace ControlEx
{
    public static class ExtendControlFunction
    {
        private static Dictionary<Control, LoadingControl> lst = new Dictionary<Control, LoadingControl>();
        public static bool BeginLoadDate(this Control c)
        {
            if (!lst.ContainsKey(c))
            {
                LoadingControl lc = new LoadingControl();
                lc.Location = Point.Empty;
                lc.Size = c.Size;
                c.Controls.Add(lc);
                lc.BringToFront();
                //当大小改变的时候重置大小
                c.SizeChanged += (object sender, EventArgs e) =>
                    {
                        lc.Size = c.Size;
                    };
                lst.Add(c, lc);
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void EndLoadDate(this Control c)
        {
            if (lst.ContainsKey(c))
            {
                LoadingControl lc = lst[c];
                lc.Dispose();
                lst.Remove(c);
            }
        }

        /// <summary>
        /// 不带参数取数据的方法
        /// </summary>
        /// <typeparam name="TBindingDataType"></typeparam>
        /// <param name="c">显示Loading的控件</param>
        /// <param name="f">取数据</param>
        /// <param name="a">绑定数据</param>
        public static void LoadDataAsyn<TBindingDataType>(this Control c, Func<TBindingDataType> f, Action<TBindingDataType> a)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {

                TBindingDataType result = f();
                e.Result = result;

            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                c.EndLoadDate();
                if (e.Error == null)
                {
                    a((TBindingDataType)e.Result);
                }
                else
                {
                    throw e.Error;
                }
            };
            if (c.BeginLoadDate())
            {
                worker.RunWorkerAsync();
            }
            else
            {
                worker.Dispose();
            }
        }

    }
}
