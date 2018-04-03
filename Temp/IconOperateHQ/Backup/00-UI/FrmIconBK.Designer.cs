namespace Shaka.UI
{
    partial class FrmIconBK
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
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUpload = new System.Windows.Forms.Button();
            this.txtFold = new System.Windows.Forms.TextBox();
            this.lblFile = new System.Windows.Forms.Label();
            this.dgvIcon = new System.Windows.Forms.DataGridView();
            this.ucPagination = new Shaka.UI.UC.UCPagination();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(499, 24);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(32, 23);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(173, 55);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUpload);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtFold);
            this.groupBox1.Controls.Add(this.lblFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 98);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询和编辑";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(267, 55);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 7;
            this.btnUpload.Text = "上传";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // txtFold
            // 
            this.txtFold.Location = new System.Drawing.Point(77, 24);
            this.txtFold.Name = "txtFold";
            this.txtFold.Size = new System.Drawing.Size(416, 21);
            this.txtFold.TabIndex = 5;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(17, 27);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(41, 12);
            this.lblFile.TabIndex = 4;
            this.lblFile.Text = "文件：";
            // 
            // dgvIcon
            // 
            this.dgvIcon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIcon.Location = new System.Drawing.Point(12, 116);
            this.dgvIcon.MultiSelect = false;
            this.dgvIcon.Name = "dgvIcon";
            this.dgvIcon.ReadOnly = true;
            this.dgvIcon.RowTemplate.Height = 23;
            this.dgvIcon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIcon.Size = new System.Drawing.Size(828, 334);
            this.dgvIcon.TabIndex = 10;
            // 
            // ucPagination
            // 
            this.ucPagination.CurrentPage = 1;
            this.ucPagination.Location = new System.Drawing.Point(12, 456);
            this.ucPagination.Name = "ucPagination";
            this.ucPagination.PageRecordCount = 5;
            this.ucPagination.Size = new System.Drawing.Size(828, 25);
            this.ucPagination.TabIndex = 11;
            this.ucPagination.TotalPageCount = 0;
            this.ucPagination.TotalRecordCount = 0;
            // 
            // FrmIconHQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 491);
            this.Controls.Add(this.ucPagination);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvIcon);
            this.Name = "FrmIconHQ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmIconBK";
            this.Load += new System.EventHandler(this.FrmIcon_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIcon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFold;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.DataGridView dgvIcon;
        private Shaka.UI.UC.UCPagination ucPagination;
        private System.Windows.Forms.Button btnUpload;

    }
}