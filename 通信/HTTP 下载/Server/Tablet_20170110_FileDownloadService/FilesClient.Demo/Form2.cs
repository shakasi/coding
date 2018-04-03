using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Net;
using System.ServiceModel;

namespace FilesClient.Demo
{
    public partial class Form2 : Form
    {
        string _savefile, _serverfile;
        int buffersize = 1024;
        int div;
        long position;
        public Form2(string savefile, string serverfile, long filelength)
        {
            InitializeComponent();
            button1.Text = "重试";
            button1.Enabled = false;
            if (filelength > int.MaxValue)
            {
                div = (int)Math.Log10(filelength) - 9;
                div = div < 0 ? 0 : div;
            }
            //progressBar1.Maximum = (int)(filelength >> div);
            progressBar1.Maximum = 0;//(int) DataManager.Channel.GetFile(serverfile);
            _savefile = savefile;
            _serverfile = serverfile;
            new Action(Download).BeginInvoke(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Action(Download).BeginInvoke(null, null);
            button1.Enabled = false;
        }

        void Download()
        {
            try
            {
                //var fs = new FileStream(_savefile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                var fs = new FileStream(_savefile, FileMode.Open, FileAccess.Read, FileShare.Read);
                fs.Position = position;
                var stream = DataManager.Channel.DownloadFileStream(_serverfile, position);
                CopyStream(stream, fs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void CopyStream(Stream fromstream, Stream tostream)
        {
            byte[] buffer = new byte[buffersize];
            int count = 0, offset = 0;
            try
            {
                while ((count = fromstream.Read(buffer, offset, buffersize - offset)) > 0)
                {
                    offset += count;
                    if (offset == buffersize)
                    {
                        tostream.Write(buffer, 0, offset);
                        offset = 0;
                        SetProgressBar1Value((int)(tostream.Position >> div));
                    }
                }
                if (offset > 0)
                    tostream.Write(buffer, 0, offset);
                SetProgressBar1Value((int)(tostream.Position >> div));
                DataManager.SetControlText(this, "下载完成，2秒后自动关闭");
                Thread.Sleep(2000);
                this.Invoke(new Action(Close));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show("下载时出错，请重试或取消下载");
                this.button1.Invoke(new Action(() => this.button1.Enabled = true));
            }
            finally
            {
                position = tostream.Position;
                fromstream.Dispose();
                tostream.Dispose();
            }
        }

        void SetProgressBar1Value(int value)
        {
            if (progressBar1.InvokeRequired && progressBar1.IsHandleCreated)
                progressBar1.BeginInvoke(new Action<int>(SetProgressBar1Value), value);
            else
                progressBar1.Value = value;
        }
    }
}
