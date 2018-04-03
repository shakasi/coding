using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Shown(object sender, EventArgs e)
		{
			this.fileSystemWatcher1.Path = Environment.CurrentDirectory;
			this.fileSystemWatcher1.Filter = "RunOptions.xml";
			this.fileSystemWatcher1.NotifyFilter = System.IO.NotifyFilters.LastWrite;
			this.fileSystemWatcher1.EnableRaisingEvents = true;			
		}

		private void fileSystemWatcher1_Changed(object sender, System.IO.FileSystemEventArgs e)
		{
			string message = string.Format("{0} {1}.", e.Name, e.ChangeType);
			this.listBox1.Items.Add(message);
		}

		
	}
}
