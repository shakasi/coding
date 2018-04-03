namespace Shaka.UI.UC
{
    partial class UCPagination
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabel_FirstPage = new System.Windows.Forms.LinkLabel();
            this.linkLabel_PreviousPage = new System.Windows.Forms.LinkLabel();
            this.linkLabel_NextPage = new System.Windows.Forms.LinkLabel();
            this.linkLabel_LastPage = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_TotalPageCount = new System.Windows.Forms.Label();
            this.linkLabel_Go = new System.Windows.Forms.LinkLabel();
            this.label_TotalRecordCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_CurrentPage = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // linkLabel_FirstPage
            // 
            this.linkLabel_FirstPage.AutoSize = true;
            this.linkLabel_FirstPage.Location = new System.Drawing.Point(6, 6);
            this.linkLabel_FirstPage.Name = "linkLabel_FirstPage";
            this.linkLabel_FirstPage.Size = new System.Drawing.Size(41, 12);
            this.linkLabel_FirstPage.TabIndex = 0;
            this.linkLabel_FirstPage.TabStop = true;
            this.linkLabel_FirstPage.Text = "第一页";
            this.linkLabel_FirstPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_FirstPage_LinkClicked);
            // 
            // linkLabel_PreviousPage
            // 
            this.linkLabel_PreviousPage.AutoSize = true;
            this.linkLabel_PreviousPage.Location = new System.Drawing.Point(54, 6);
            this.linkLabel_PreviousPage.Name = "linkLabel_PreviousPage";
            this.linkLabel_PreviousPage.Size = new System.Drawing.Size(41, 12);
            this.linkLabel_PreviousPage.TabIndex = 1;
            this.linkLabel_PreviousPage.TabStop = true;
            this.linkLabel_PreviousPage.Text = "上一页";
            this.linkLabel_PreviousPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_PreviousPage_LinkClicked);
            // 
            // linkLabel_NextPage
            // 
            this.linkLabel_NextPage.AutoSize = true;
            this.linkLabel_NextPage.Location = new System.Drawing.Point(270, 6);
            this.linkLabel_NextPage.Name = "linkLabel_NextPage";
            this.linkLabel_NextPage.Size = new System.Drawing.Size(41, 12);
            this.linkLabel_NextPage.TabIndex = 3;
            this.linkLabel_NextPage.TabStop = true;
            this.linkLabel_NextPage.Text = "下一页";
            this.linkLabel_NextPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_NextPage_LinkClicked);
            // 
            // linkLabel_LastPage
            // 
            this.linkLabel_LastPage.AutoSize = true;
            this.linkLabel_LastPage.Location = new System.Drawing.Point(316, 6);
            this.linkLabel_LastPage.Name = "linkLabel_LastPage";
            this.linkLabel_LastPage.Size = new System.Drawing.Size(53, 12);
            this.linkLabel_LastPage.TabIndex = 4;
            this.linkLabel_LastPage.TabStop = true;
            this.linkLabel_LastPage.Text = "最后一页";
            this.linkLabel_LastPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LastPage_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "Page";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "of";
            // 
            // label_TotalPageCount
            // 
            this.label_TotalPageCount.AutoSize = true;
            this.label_TotalPageCount.Location = new System.Drawing.Point(197, 6);
            this.label_TotalPageCount.Name = "label_TotalPageCount";
            this.label_TotalPageCount.Size = new System.Drawing.Size(11, 12);
            this.label_TotalPageCount.TabIndex = 7;
            this.label_TotalPageCount.Text = "0";
            // 
            // linkLabel_Go
            // 
            this.linkLabel_Go.AutoSize = true;
            this.linkLabel_Go.Location = new System.Drawing.Point(232, 6);
            this.linkLabel_Go.Name = "linkLabel_Go";
            this.linkLabel_Go.Size = new System.Drawing.Size(17, 12);
            this.linkLabel_Go.TabIndex = 8;
            this.linkLabel_Go.TabStop = true;
            this.linkLabel_Go.Text = "Go";
            this.linkLabel_Go.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Go_LinkClicked);
            // 
            // label_TotalRecordCount
            // 
            this.label_TotalRecordCount.AutoSize = true;
            this.label_TotalRecordCount.Location = new System.Drawing.Point(446, 6);
            this.label_TotalRecordCount.Name = "label_TotalRecordCount";
            this.label_TotalRecordCount.Size = new System.Drawing.Size(11, 12);
            this.label_TotalRecordCount.TabIndex = 9;
            this.label_TotalRecordCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(375, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "总记录数：";
            // 
            // txt_CurrentPage
            // 
            this.txt_CurrentPage.Location = new System.Drawing.Point(132, 2);
            this.txt_CurrentPage.Name = "txt_CurrentPage";
            this.txt_CurrentPage.Size = new System.Drawing.Size(44, 21);
            this.txt_CurrentPage.TabIndex = 2;
            // 
            // UCPagination
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_TotalRecordCount);
            this.Controls.Add(this.linkLabel_Go);
            this.Controls.Add(this.label_TotalPageCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel_LastPage);
            this.Controls.Add(this.linkLabel_NextPage);
            this.Controls.Add(this.txt_CurrentPage);
            this.Controls.Add(this.linkLabel_PreviousPage);
            this.Controls.Add(this.linkLabel_FirstPage);
            this.Name = "UCPagination";
            this.Size = new System.Drawing.Size(698, 25);
            this.Load += new System.EventHandler(this.UCPagination_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel_FirstPage;
        private System.Windows.Forms.LinkLabel linkLabel_PreviousPage;
        private System.Windows.Forms.TextBox txt_CurrentPage;
        private System.Windows.Forms.LinkLabel linkLabel_NextPage;
        private System.Windows.Forms.LinkLabel linkLabel_LastPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_TotalPageCount;
        private System.Windows.Forms.LinkLabel linkLabel_Go;
        private System.Windows.Forms.Label label_TotalRecordCount;
        private System.Windows.Forms.Label label3;
    }
}
