namespace Shaka.UI
{
    partial class FrmIconOperateHQ
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
            this.lblUserType = new System.Windows.Forms.Label();
            this.rdbStoreGroup = new System.Windows.Forms.RadioButton();
            this.lblEffectiveDate = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEffectiveDate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvStoreGroup = new System.Windows.Forms.DataGridView();
            this.ucPagination = new Shaka.UI.UC.UCPagination();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoreGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Location = new System.Drawing.Point(43, 15);
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.Size = new System.Drawing.Size(71, 12);
            this.lblUserType.TabIndex = 4;
            this.lblUserType.Text = "User Type：";
            // 
            // rdbStoreGroup
            // 
            this.rdbStoreGroup.AutoSize = true;
            this.rdbStoreGroup.Checked = true;
            this.rdbStoreGroup.Location = new System.Drawing.Point(120, 13);
            this.rdbStoreGroup.Name = "rdbStoreGroup";
            this.rdbStoreGroup.Size = new System.Drawing.Size(89, 16);
            this.rdbStoreGroup.TabIndex = 5;
            this.rdbStoreGroup.TabStop = true;
            this.rdbStoreGroup.Text = "Store Group";
            this.rdbStoreGroup.UseVisualStyleBackColor = true;
            // 
            // lblEffectiveDate
            // 
            this.lblEffectiveDate.AutoSize = true;
            this.lblEffectiveDate.Location = new System.Drawing.Point(19, 41);
            this.lblEffectiveDate.Name = "lblEffectiveDate";
            this.lblEffectiveDate.Size = new System.Drawing.Size(95, 12);
            this.lblEffectiveDate.TabIndex = 6;
            this.lblEffectiveDate.Text = "EffectiveDate：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(237, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "*";
            // 
            // dtpEffectiveDate
            // 
            this.dtpEffectiveDate.Location = new System.Drawing.Point(120, 35);
            this.dtpEffectiveDate.Name = "dtpEffectiveDate";
            this.dtpEffectiveDate.Size = new System.Drawing.Size(111, 21);
            this.dtpEffectiveDate.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblUserType);
            this.panel1.Controls.Add(this.dtpEffectiveDate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblEffectiveDate);
            this.panel1.Controls.Add(this.rdbStoreGroup);
            this.panel1.Location = new System.Drawing.Point(24, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(716, 73);
            this.panel1.TabIndex = 9;
            // 
            // dgvStoreGroup
            // 
            this.dgvStoreGroup.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStoreGroup.Location = new System.Drawing.Point(24, 95);
            this.dgvStoreGroup.MultiSelect = false;
            this.dgvStoreGroup.Name = "dgvStoreGroup";
            this.dgvStoreGroup.ReadOnly = true;
            this.dgvStoreGroup.RowTemplate.Height = 23;
            this.dgvStoreGroup.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStoreGroup.Size = new System.Drawing.Size(716, 279);
            this.dgvStoreGroup.TabIndex = 11;
            this.dgvStoreGroup.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStoreGroup_CellClick);
            // 
            // ucPagination
            // 
            this.ucPagination.CurrentPage = 1;
            this.ucPagination.Location = new System.Drawing.Point(24, 382);
            this.ucPagination.Name = "ucPagination";
            this.ucPagination.PageRecordCount = 5;
            this.ucPagination.Size = new System.Drawing.Size(716, 25);
            this.ucPagination.TabIndex = 10;
            this.ucPagination.TotalPageCount = 0;
            this.ucPagination.TotalRecordCount = 0;
            // 
            // FrmIconOperateHQ
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 417);
            this.Controls.Add(this.dgvStoreGroup);
            this.Controls.Add(this.ucPagination);
            this.Controls.Add(this.panel1);
            this.Name = "FrmIconOperateHQ";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "（HQ）Icon上传";
            this.Load += new System.EventHandler(this.FrmIconOperateHQ_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStoreGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblUserType;
        private System.Windows.Forms.Label lblEffectiveDate;
        private System.Windows.Forms.RadioButton rdbStoreGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEffectiveDate;
        private System.Windows.Forms.Panel panel1;
        private Shaka.UI.UC.UCPagination ucPagination;
        private System.Windows.Forms.DataGridView dgvStoreGroup;
    }
}