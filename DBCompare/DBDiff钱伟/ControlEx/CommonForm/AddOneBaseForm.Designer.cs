namespace ControlEx.CommonForm
{
    partial class AddOneBaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnCloseView = new System.Windows.Forms.ToolStripButton();
            this.BtnAutoHide = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.AddOneText = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnCloseView,
            this.BtnAutoHide,
            this.toolStripButton1,
            this.AddOneText});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(216, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnCloseView
            // 
            this.btnCloseView.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.btnCloseView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCloseView.Image = global::ControlEx.Properties.Resources.close_view;
            this.btnCloseView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCloseView.Name = "btnCloseView";
            this.btnCloseView.Size = new System.Drawing.Size(23, 22);
            this.btnCloseView.Text = "关闭";
            this.btnCloseView.Click += new System.EventHandler(this.btnCloseView_Click);
            // 
            // BtnAutoHide
            // 
            this.BtnAutoHide.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.BtnAutoHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnAutoHide.Image = global::ControlEx.Properties.Resources.pin_view;
            this.BtnAutoHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAutoHide.Name = "BtnAutoHide";
            this.BtnAutoHide.Size = new System.Drawing.Size(23, 22);
            this.BtnAutoHide.Text = "AutoHide";
            this.BtnAutoHide.Click += new System.EventHandler(this.BtnAutoHide_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::ControlEx.Properties.Resources.refresh;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "刷新";
            // 
            // AddOneText
            // 
            this.AddOneText.Name = "AddOneText";
            this.AddOneText.Size = new System.Drawing.Size(0, 22);
            // 
            // AddOneBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(216, 531);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddOneBaseForm";
            this.Text = "AddOneBaseForm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton btnCloseView;
        private System.Windows.Forms.ToolStripButton BtnAutoHide;
        private System.Windows.Forms.ToolStripLabel AddOneText;
    }
}