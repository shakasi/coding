namespace MessageQueueReceive
{
    partial class FrmReceive
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
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.btnReceive = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtMsg
            // 
            this.txtMsg.Location = new System.Drawing.Point(12, 25);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(249, 21);
            this.txtMsg.TabIndex = 3;
            // 
            // btnReceive
            // 
            this.btnReceive.Location = new System.Drawing.Point(186, 106);
            this.btnReceive.Name = "btnReceive";
            this.btnReceive.Size = new System.Drawing.Size(75, 23);
            this.btnReceive.TabIndex = 2;
            this.btnReceive.Text = "接收";
            this.btnReceive.UseVisualStyleBackColor = true;
            this.btnReceive.Click += new System.EventHandler(this.btnReceive_Click);
            // 
            // FrmReceive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(276, 149);
            this.Controls.Add(this.txtMsg);
            this.Controls.Add(this.btnReceive);
            this.Name = "FrmReceive";
            this.Text = "接收";
            this.Load += new System.EventHandler(this.FrmReceive_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.Button btnReceive;
    }
}

