namespace Shaka.BackgroundWorkerTest
{
    partial class frmBackgroundWorkerTest
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnGoOn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.nudResult = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudThreadCount = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadCount)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(81, 100);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(84, 39);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "开始";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(421, 100);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(193, 100);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(85, 40);
            this.btnPause.TabIndex = 2;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnGoOn
            // 
            this.btnGoOn.Location = new System.Drawing.Point(305, 100);
            this.btnGoOn.Name = "btnGoOn";
            this.btnGoOn.Size = new System.Drawing.Size(88, 40);
            this.btnGoOn.TabIndex = 3;
            this.btnGoOn.Text = "继续";
            this.btnGoOn.UseVisualStyleBackColor = true;
            this.btnGoOn.Click += new System.EventHandler(this.btnGoOn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(305, 59);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(194, 23);
            this.progressBar1.TabIndex = 6;
            // 
            // nudResult
            // 
            this.nudResult.Location = new System.Drawing.Point(146, 61);
            this.nudResult.Name = "nudResult";
            this.nudResult.Size = new System.Drawing.Size(132, 21);
            this.nudResult.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "线程结果";
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.ForeColor = System.Drawing.Color.Red;
            this.lblResult.Location = new System.Drawing.Point(86, 160);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(0, 12);
            this.lblResult.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "线程数";
            // 
            // nudThreadCount
            // 
            this.nudThreadCount.Location = new System.Drawing.Point(145, 26);
            this.nudThreadCount.Name = "nudThreadCount";
            this.nudThreadCount.Size = new System.Drawing.Size(132, 21);
            this.nudThreadCount.TabIndex = 8;
            this.nudThreadCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 155);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "调试状态";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmBackgroundWorkerTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 191);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudThreadCount);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudResult);
            this.Controls.Add(this.btnGoOn);
            this.Controls.Add(this.btnPause);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnGo);
            this.Name = "frmBackgroundWorkerTest";
            this.Text = "多线程";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreadCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.Button btnGoOn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.NumericUpDown nudResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudThreadCount;
        private System.Windows.Forms.Button button1;
    }
}

