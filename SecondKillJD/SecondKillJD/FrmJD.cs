using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SecondKillJD
{
    public partial class FrmJD : Form
    {
        private string _id = string.Empty;
        private string _pwd = string.Empty;
        private string _goodLink = string.Empty;
        private DateTime _secondKillTime = new DateTime();

        public FrmJD()
        {
            InitializeComponent();
        }

        private void FrmJD_Load(object sender, EventArgs e)
        {
            this.txtID.Text = "sjs0512@163.com";
            this.txtPWD.Text = "s456J852s";
            this.dtpSecondKillTime.Value = DateTime.Now;

            System.Timers.Timer t = new System.Timers.Timer(1000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(GetSecondKill);
            t.AutoReset = true;
            t.Enabled = true;
        }

        private void GetSecondKill(object sender, System.Timers.ElapsedEventArgs e)
        {
            _secondKillTime = this.dtpSecondKillTime.Value;
            TimeSpan ts = DateTime.Now - _secondKillTime;
            if (ts.Days == 0 && ts.Hours == 0 && ts.Minutes == 0
                && ts.Seconds <= 2 && ts.Seconds >= 1)
            {

            }
        }
    }
}