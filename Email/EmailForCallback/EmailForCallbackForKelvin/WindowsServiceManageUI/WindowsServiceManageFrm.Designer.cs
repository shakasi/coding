namespace WindowsServiceManageUI
{
    partial class WindowsServiceManageFrm
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
            this.btnInstall = new System.Windows.Forms.Button();
            this.btnUninstall = new System.Windows.Forms.Button();
            this.lblLog = new System.Windows.Forms.Label();
            this.btnPauseContinue = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpDOTime = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(17, 20);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(84, 32);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "安装";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // btnUninstall
            // 
            this.btnUninstall.Location = new System.Drawing.Point(123, 20);
            this.btnUninstall.Name = "btnUninstall";
            this.btnUninstall.Size = new System.Drawing.Size(84, 32);
            this.btnUninstall.TabIndex = 0;
            this.btnUninstall.Text = "卸载";
            this.btnUninstall.UseVisualStyleBackColor = true;
            this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
            // 
            // lblLog
            // 
            this.lblLog.AutoSize = true;
            this.lblLog.ForeColor = System.Drawing.Color.Red;
            this.lblLog.Location = new System.Drawing.Point(24, 12);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(0, 12);
            this.lblLog.TabIndex = 1;
            // 
            // btnPauseContinue
            // 
            this.btnPauseContinue.Location = new System.Drawing.Point(17, 66);
            this.btnPauseContinue.Name = "btnPauseContinue";
            this.btnPauseContinue.Size = new System.Drawing.Size(131, 32);
            this.btnPauseContinue.TabIndex = 0;
            this.btnPauseContinue.Text = "Pause/Continue";
            this.btnPauseContinue.UseVisualStyleBackColor = true;
            this.btnPauseContinue.Click += new System.EventHandler(this.btnPauseContinue_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.dtpDOTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(517, 65);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "配置服务挂载程序";
            // 
            // dtpDOTime
            // 
            this.dtpDOTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDOTime.Location = new System.Drawing.Point(84, 20);
            this.dtpDOTime.Name = "dtpDOTime";
            this.dtpDOTime.ShowUpDown = true;
            this.dtpDOTime.Size = new System.Drawing.Size(83, 21);
            this.dtpDOTime.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "执行时间：";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnInstall);
            this.groupBox2.Controls.Add(this.btnUninstall);
            this.groupBox2.Controls.Add(this.btnPauseContinue);
            this.groupBox2.Location = new System.Drawing.Point(12, 113);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(517, 114);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作系统服务";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(188, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 32);
            this.button1.TabIndex = 5;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnSave);
            // 
            // WindowsServiceManageFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 242);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblLog);
            this.Name = "WindowsServiceManageFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WindowsServiceManage";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Button btnUninstall;
        private System.Windows.Forms.Label lblLog;
        private System.Windows.Forms.Button btnPauseContinue;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpDOTime;
        private System.Windows.Forms.Button button1;
    }
}

