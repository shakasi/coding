namespace WindowsFormsApplication1
{
	partial class Form1
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
			if( disposing && (components != null) ) {
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(579, 35);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(425, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "请修改程序所在目录下的RunOptions.xml文件，本程序将会显示所监视到的结果";
			// 
			// listBox1
			// 
			this.listBox1.BackColor = System.Drawing.SystemColors.Control;
			this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(0, 35);
			this.listBox1.Name = "listBox1";
			this.listBox1.ScrollAlwaysVisible = true;
			this.listBox1.Size = new System.Drawing.Size(579, 304);
			this.listBox1.TabIndex = 1;
			// 
			// fileSystemWatcher1
			// 
			this.fileSystemWatcher1.SynchronizingObject = this;
			this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(579, 340);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "FileSystemWatcher DEMO  - http://www.cnblogs.com/fish-li/";
			this.Shown += new System.EventHandler(this.Form1_Shown);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox listBox1;
		private System.IO.FileSystemWatcher fileSystemWatcher1;
	}
}

