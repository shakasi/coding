using ControlEx.CommonForm;
using ControlEx.Entity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExtendFuction;
using System.ComponentModel;
using ControlEx.Designer;

namespace ControlEx.BasicControlEx
{
    [Designer(typeof(HideUnnecessaryPropertyDesign))]
    public class DockControl : ToolStrip
    {
        private Dictionary<string,PanelEx> PlugInPanel;//
        public DockControl()
        {
            PlugInPanel = new Dictionary<string, PanelEx>();
            this.SizeChanged += (object sender, EventArgs e) => 
            {
                RelocateTheAddOnes();
            };
            this.LocationChanged += (object sender, EventArgs e) =>
            {
                RelocateTheAddOnes();
            };
            this.ParentChanged += (object sender, EventArgs e) =>
                {
                    SetVisibleState();
                    this.GripStyle = ToolStripGripStyle.Hidden;
                    this.TextDirection = ToolStripTextDirection.Vertical90;
                };
        }

        public bool Add(DockControlEntity entity)
        {

            if (!this.FindForm().IsMdiContainer)
            {
                throw new NotSupportedException("DockControl只支持MID窗体");
            }
            PanelEx panel = new PanelEx();
            panel.ID = Guid.NewGuid().ToString();
            switch (this.Dock)
            {
                case DockStyle.Left:
                    {
                        panel.AllowExtendRight = true;
                        panel.Location = new System.Drawing.Point(this.Location.X + this.Width, this.Location.Y);
                        panel.Size = new System.Drawing.Size(120, this.Height);
                        break;
                    }
                case DockStyle.Right:
                    {
                        panel.AllowExtendLeft = true;
                        panel.Size = new System.Drawing.Size(120, this.Height);
                        panel.Location = new System.Drawing.Point(this.Location.X - panel.Width, this.Location.Y);
                        break;
                    }
                default: throw new NotSupportedException("目前只支持向左和向右停靠");
            }
            if (entity.IsAutoHide)
            {
                panel.Visible = false;
                AddButtonToToolTrip(panel, entity);
            }
            else
            {
                panel.Visible = true;
            }
            this.FindForm().Controls.Add(panel);
            //panel.BeginLoadDate();
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += (object sender1111, DoWorkEventArgs e111) =>
            //    {
            AddOneBaseForm frm = entity.DisplayForm;
            // AddOneBaseForm frm=(AddOneBaseForm) Activator.CreateInstance(entity.DisplayForm.GetType());
            frm.IsAutoHide = entity.IsAutoHide;
            frm.ID = panel.ID;
            frm.FrmText = entity.MenuText;
            frm.MdiParent = this.FindForm();
            frm.Parent = panel;
            panel.Controls.Add(frm);
            frm.Dock = DockStyle.Fill;
            frm.Width = panel.Width;
            frm.Show();
            frm.BringToFront();
            PlugInPanel.Add(panel.ID, panel);
            panel.BringToFront();
            if (!entity.IsAutoHide)
            {
                frm.Parent.Dock = this.Dock;
            }
            else
            {
                frm.Parent.Dock = DockStyle.None;
            }

            frm.AddOneAutoHideChanged += (object sender, EventArgs e) =>
            {
                frm.IsAutoHide = !frm.IsAutoHide; //最新的autoHide
                if (!frm.IsAutoHide)
                {
                    frm.Parent.Dock = this.Dock;
                    RemoveButtonToToolTrip(panel, entity);
                }
                else
                {
                    frm.Parent.Dock = DockStyle.None;
                    AddButtonToToolTrip(panel, entity);
                    frm.Parent.Visible = false;
                }
            };
            panel.MouseLeaveExtend(
                () =>
                {
                    if (frm.IsAutoHide)
                    {
                        PlugInPanel[frm.ID].Visible = false;
                    }
                });
            frm.AddOneClosed += (object sender, EventArgs e) =>
            {
                PlugInPanel[frm.ID].Dispose();
                PlugInPanel.Remove(frm.ID);
                RemoveButtonToToolTrip(panel, entity);
                frm.Close();
            };
            //    };
            //worker.RunWorkerCompleted += (object sender ,RunWorkerCompletedEventArgs e) => {
            //};
            //worker.RunWorkerAsync();
            return true;
        }

        //添加一个按钮（autohide的时候）
        private void AddButtonToToolTrip(PanelEx panel,DockControlEntity entity)
        {
           //  panel.Visible = false;
            ToolStripButton btn = new ToolStripButton();
            btn.Name = panel.ID;
            btn.Text = entity.MenuText;
            btn.Click += (object sender, EventArgs e) =>
            {
                panel.Visible = true;
                panel.BringToFront();
                if (panel.CanFocus)
                {
                    panel.Focus();
                }
            };
            this.Items.Add(btn);
            this.Visible = true;
        }

        //当订到界面上时
        private void RemoveButtonToToolTrip(PanelEx panel, DockControlEntity entity)
        {
           this.Items.RemoveByKey(panel.ID);
           SetVisibleState();
        }
        //设置当前的控件是否可见（根据是否有项 ）
        private void SetVisibleState()
        {
            if (this.Items.Count == 0)
            {
                this.Visible = false;
            }
            else
            {
                this.Visible = true;
            }
        }
        public bool AddList(ICollection<DockControlEntity> entities)
        {
            foreach (DockControlEntity dce in entities)
            {
                Add(dce);
            }
            return true;
        }
 
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void RelocateTheAddOnes()
        {
            foreach (KeyValuePair<string, PanelEx> keyvalue in PlugInPanel)
            {
                if (this.Dock == DockStyle.Left)
                {
                    keyvalue.Value.Location = new System.Drawing.Point(this.Location.X + this.Width, this.Location.Y);
                    keyvalue.Value.Height = this.Height;
                }
                else if (this.Dock == DockStyle.Right)
                {
                    keyvalue.Value.Size = new System.Drawing.Size(120, this.Height);
                    keyvalue.Value.Location = new System.Drawing.Point(this.Location.X - keyvalue.Value.Width, this.Location.Y);
                }

            }
        }


    }
}
