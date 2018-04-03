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
            this.dgvIcon = new System.Windows.Forms.DataGridView();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.txtFold = new System.Windows.Forms.TextBox();
            this.lblFiles = new System.Windows.Forms.Label();
            this.ucPagination = new Shaka.UI.UC.UCPagination();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIcon)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvIcon
            // 
            this.dgvIcon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvIcon.Location = new System.Drawing.Point(12, 115);
            this.dgvIcon.MultiSelect = false;
            this.dgvIcon.Name = "dgvIcon";
            this.dgvIcon.ReadOnly = true;
            this.dgvIcon.RowTemplate.Height = 23;
            this.dgvIcon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIcon.Size = new System.Drawing.Size(828, 270);
            this.dgvIcon.TabIndex = 13;
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
            this.btnSave.Location = new System.Drawing.Point(465, 56);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(66, 33);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMsg);
            this.groupBox1.Controls.Add(this.btnBrowse);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtFold);
            this.groupBox1.Controls.Add(this.lblFiles);
            this.groupBox1.Location = new System.Drawing.Point(12, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(828, 98);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询和编辑";
            // 
            // lblMsg
            // 
            this.lblMsg.AutoSize = true;
            this.lblMsg.ForeColor = System.Drawing.Color.Crimson;
            this.lblMsg.Location = new System.Drawing.Point(17, 66);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 12);
            this.lblMsg.TabIndex = 7;
            // 
            // txtFold
            // 
            this.txtFold.Location = new System.Drawing.Point(77, 24);
            this.txtFold.Name = "txtFold";
            this.txtFold.Size = new System.Drawing.Size(416, 21);
            this.txtFold.TabIndex = 5;
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(17, 27);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(53, 12);
            this.lblFiles.TabIndex = 4;
            this.lblFiles.Text = "文件夹：";
            // 
            // ucPagination
            // 
            this.ucPagination.CurrentPage = 1;
            this.ucPagination.Location = new System.Drawing.Point(12, 394);
            this.ucPagination.Name = "ucPagination";
            this.ucPagination.PageSize = 5;
            this.ucPagination.Size = new System.Drawing.Size(828, 25);
            this.ucPagination.TabIndex = 14;
            this.ucPagination.TotalPageCount = 0;
            this.ucPagination.TotalRecordCount = 0;
            // 
            // FrmIconBK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 425);
            this.Controls.Add(this.ucPagination);
            this.Controls.Add(this.dgvIcon);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmIconBK";
            this.Text = "FrmIconBK(一次超过100张图会慢)";
            this.Load += new System.EventHandler(this.FrmIconBK_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIcon)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private UC.UCPagination ucPagination;
        private System.Windows.Forms.DataGridView dgvIcon;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.TextBox txtFold;
        private System.Windows.Forms.Label lblFiles;
    }
}