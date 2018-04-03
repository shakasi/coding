namespace Transight.HQV4.HQService
{
    partial class ConsoleFrm
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
            this.gbCl = new System.Windows.Forms.GroupBox();
            this.lvList = new System.Windows.Forms.ListView();
            this.gbFun = new System.Windows.Forms.GroupBox();
            this.btnUPBizDate = new System.Windows.Forms.Button();
            this.btnOtherUpload = new System.Windows.Forms.Button();
            this.btnAutoUpdte = new System.Windows.Forms.Button();
            this.btnCallBack = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnUpdImport = new System.Windows.Forms.Button();
            this.btnIenImport = new System.Windows.Forms.Button();
            this.btnIendl = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtMsg = new System.Windows.Forms.RichTextBox();
            this.gbCl.SuspendLayout();
            this.gbFun.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbCl
            // 
            this.gbCl.Controls.Add(this.lvList);
            this.gbCl.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbCl.Location = new System.Drawing.Point(0, 0);
            this.gbCl.Name = "gbCl";
            this.gbCl.Size = new System.Drawing.Size(490, 510);
            this.gbCl.TabIndex = 0;
            this.gbCl.TabStop = false;
            this.gbCl.Text = "门店客户端列表";
            // 
            // lvList
            // 
            this.lvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvList.Location = new System.Drawing.Point(3, 17);
            this.lvList.Name = "lvList";
            this.lvList.Size = new System.Drawing.Size(484, 490);
            this.lvList.TabIndex = 0;
            this.lvList.UseCompatibleStateImageBehavior = false;
            // 
            // gbFun
            // 
            this.gbFun.Controls.Add(this.btnUPBizDate);
            this.gbFun.Controls.Add(this.btnOtherUpload);
            this.gbFun.Controls.Add(this.btnAutoUpdte);
            this.gbFun.Controls.Add(this.btnCallBack);
            this.gbFun.Controls.Add(this.btnUpload);
            this.gbFun.Controls.Add(this.btnDownload);
            this.gbFun.Controls.Add(this.btnUpdImport);
            this.gbFun.Controls.Add(this.btnIenImport);
            this.gbFun.Controls.Add(this.btnIendl);
            this.gbFun.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbFun.Location = new System.Drawing.Point(490, 0);
            this.gbFun.Name = "gbFun";
            this.gbFun.Size = new System.Drawing.Size(605, 103);
            this.gbFun.TabIndex = 1;
            this.gbFun.TabStop = false;
            this.gbFun.Text = "功能区";
            // 
            // btnUPBizDate
            // 
            this.btnUPBizDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUPBizDate.Location = new System.Drawing.Point(369, 54);
            this.btnUPBizDate.Name = "btnUPBizDate";
            this.btnUPBizDate.Size = new System.Drawing.Size(100, 28);
            this.btnUPBizDate.TabIndex = 20;
            this.btnUPBizDate.Text = "更新营业日";
            this.btnUPBizDate.UseVisualStyleBackColor = true;
            this.btnUPBizDate.Click += new System.EventHandler(this.btnUPBizDate_Click);
            // 
            // btnOtherUpload
            // 
            this.btnOtherUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOtherUpload.Location = new System.Drawing.Point(486, 20);
            this.btnOtherUpload.Name = "btnOtherUpload";
            this.btnOtherUpload.Size = new System.Drawing.Size(100, 28);
            this.btnOtherUpload.TabIndex = 19;
            this.btnOtherUpload.Text = "其他数据上传";
            this.btnOtherUpload.UseVisualStyleBackColor = true;
            this.btnOtherUpload.Click += new System.EventHandler(this.btnOtherUpload_Click);
            // 
            // btnAutoUpdte
            // 
            this.btnAutoUpdte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAutoUpdte.Location = new System.Drawing.Point(253, 54);
            this.btnAutoUpdte.Name = "btnAutoUpdte";
            this.btnAutoUpdte.Size = new System.Drawing.Size(100, 28);
            this.btnAutoUpdte.TabIndex = 17;
            this.btnAutoUpdte.Text = "自动更新";
            this.btnAutoUpdte.UseVisualStyleBackColor = true;
            this.btnAutoUpdte.Click += new System.EventHandler(this.btnAutoUpdte_Click);
            // 
            // btnCallBack
            // 
            this.btnCallBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCallBack.Location = new System.Drawing.Point(369, 20);
            this.btnCallBack.Name = "btnCallBack";
            this.btnCallBack.Size = new System.Drawing.Size(100, 28);
            this.btnCallBack.TabIndex = 16;
            this.btnCallBack.Text = "处理Ien数据反馈";
            this.btnCallBack.UseVisualStyleBackColor = true;
            this.btnCallBack.Click += new System.EventHandler(this.btnCallBack_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpload.Location = new System.Drawing.Point(137, 54);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(100, 28);
            this.btnUpload.TabIndex = 15;
            this.btnUpload.Text = "销售数据上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDownload.Location = new System.Drawing.Point(21, 54);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 28);
            this.btnDownload.TabIndex = 14;
            this.btnDownload.Text = "各类文件下载";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnUpdImport
            // 
            this.btnUpdImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnUpdImport.Location = new System.Drawing.Point(253, 20);
            this.btnUpdImport.Name = "btnUpdImport";
            this.btnUpdImport.Size = new System.Drawing.Size(100, 28);
            this.btnUpdImport.TabIndex = 13;
            this.btnUpdImport.Text = "upd表导入";
            this.btnUpdImport.UseVisualStyleBackColor = true;
            this.btnUpdImport.Click += new System.EventHandler(this.btnUpdImport_Click);
            // 
            // btnIenImport
            // 
            this.btnIenImport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIenImport.Location = new System.Drawing.Point(137, 20);
            this.btnIenImport.Name = "btnIenImport";
            this.btnIenImport.Size = new System.Drawing.Size(100, 28);
            this.btnIenImport.TabIndex = 12;
            this.btnIenImport.Text = "IEN文件导入";
            this.btnIenImport.UseVisualStyleBackColor = true;
            this.btnIenImport.Click += new System.EventHandler(this.btnIenImport_Click);
            // 
            // btnIendl
            // 
            this.btnIendl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnIendl.Location = new System.Drawing.Point(21, 20);
            this.btnIendl.Name = "btnIendl";
            this.btnIendl.Size = new System.Drawing.Size(100, 28);
            this.btnIendl.TabIndex = 11;
            this.btnIendl.Text = "IEN文件自动下载";
            this.btnIendl.UseVisualStyleBackColor = true;
            this.btnIendl.Click += new System.EventHandler(this.btnIendl_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtMsg);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(490, 103);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(605, 407);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运行日志";
            // 
            // rtxtMsg
            // 
            this.rtxtMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtMsg.Location = new System.Drawing.Point(3, 17);
            this.rtxtMsg.Name = "rtxtMsg";
            this.rtxtMsg.Size = new System.Drawing.Size(599, 387);
            this.rtxtMsg.TabIndex = 0;
            this.rtxtMsg.Text = "";
            // 
            // ConsoleFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 510);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbFun);
            this.Controls.Add(this.gbCl);
            this.Name = "ConsoleFrm";
            this.Text = "MainForm-HQ紧急下发";
            this.Load += new System.EventHandler(this.ConsoleFrm_Load);
            this.gbCl.ResumeLayout(false);
            this.gbFun.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbCl;
        private System.Windows.Forms.GroupBox gbFun;
        private System.Windows.Forms.Button btnUPBizDate;
        private System.Windows.Forms.Button btnOtherUpload;
        private System.Windows.Forms.Button btnAutoUpdte;
        private System.Windows.Forms.Button btnCallBack;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnUpdImport;
        private System.Windows.Forms.Button btnIenImport;
        private System.Windows.Forms.Button btnIendl;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtxtMsg;
        private System.Windows.Forms.ListView lvList;
    }
}

