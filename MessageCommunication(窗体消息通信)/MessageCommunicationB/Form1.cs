using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Shaka.Utils;

namespace MessageCommunicationB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void DefWndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WinMessageHelper.WM_COPYDATA:
                    string str = WinMessageHelper.Receive(ref m);
                    if (str == "close")
                    {
                        Application.Exit();
                        Environment.Exit(0);
                    }
                    else
                    {
                        MessageBox.Show(str);
                        MessageBox.Show(m.Msg.ToString());
                    }
                    break;
                default:
                    base.DefWndProc(ref m);
                    break;
            }
        }
    }
}
