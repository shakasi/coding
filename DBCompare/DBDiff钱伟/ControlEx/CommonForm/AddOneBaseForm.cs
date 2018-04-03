using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlEx.CommonForm
{
    public partial class AddOneBaseForm : Form
    {
        public AddOneBaseForm()
        {
            InitializeComponent();
        }
        public bool IsAutoHide { get; set; }

        public string ID { get; set; }

        public string FrmText {
            get { return this.AddOneText.Text; }
            set {
                this.AddOneText.Text = value;
            }
        }

        public event EventHandler AddOneAutoHideChanged;

        public event EventHandler AddOneClosed;
        private void btnCloseView_Click(object sender, EventArgs e)
        {
            AddOneClosed(this,new EventArgs());
        }

        private void BtnAutoHide_Click(object sender, EventArgs e)
        {
            if (IsAutoHide)
            {
                BtnAutoHide.Image = ControlEx.Properties.Resources.pin_view;
            }
            else
            {
                BtnAutoHide.Image = ControlEx.Properties.Resources.pinned_ovr;
            }
            AddOneAutoHideChanged(this, new EventArgs());
        }
    }
}
