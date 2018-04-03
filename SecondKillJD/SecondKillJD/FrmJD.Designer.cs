namespace SecondKillJD
{
    partial class FrmJD
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblID = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtPWD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGoodLink = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dtpSecondKillTime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(38, 24);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(41, 12);
            this.lblID.TabIndex = 0;
            this.lblID.Text = "账号：";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(94, 21);
            this.txtID.Name = "txtID";
            this.txtID.PasswordChar = '*';
            this.txtID.Size = new System.Drawing.Size(203, 21);
            this.txtID.TabIndex = 1;
            // 
            // txtPWD
            // 
            this.txtPWD.Location = new System.Drawing.Point(94, 55);
            this.txtPWD.Name = "txtPWD";
            this.txtPWD.PasswordChar = '*';
            this.txtPWD.Size = new System.Drawing.Size(203, 21);
            this.txtPWD.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "密码：";
            // 
            // txtGoodLink
            // 
            this.txtGoodLink.Location = new System.Drawing.Point(94, 18);
            this.txtGoodLink.Name = "txtGoodLink";
            this.txtGoodLink.Size = new System.Drawing.Size(686, 21);
            this.txtGoodLink.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "商品链接：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblID);
            this.panel1.Controls.Add(this.txtID);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtPWD);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(803, 90);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dtpSecondKillTime);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.txtGoodLink);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(12, 114);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(803, 100);
            this.panel2.TabIndex = 7;
            // 
            // dtpSecondKillTime
            // 
            this.dtpSecondKillTime.Location = new System.Drawing.Point(94, 46);
            this.dtpSecondKillTime.Name = "dtpSecondKillTime";
            this.dtpSecondKillTime.Size = new System.Drawing.Size(203, 21);
            this.dtpSecondKillTime.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "秒杀时间：";
            // 
            // FrmJD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 229);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmJD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "京东";
            this.Load += new System.EventHandler(this.FrmJD_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtPWD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGoodLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker dtpSecondKillTime;
        private System.Windows.Forms.Label label3;
    }
}

