using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Net;
using System.IO;
using FilesContract.Demo;
using System.Threading;

namespace FilesService.Demo
{
    public partial class Form1 : Form
    {
        string baseAddress = "http://localhos/FileDownLoadService/";
        public Form1()
        {
            InitializeComponent();
            button1.Text = "开启服务";
            button2.Text = "服务停止";
            button1.Enabled = true;
            button2.Enabled = false;
            label2.Text = AppDomain.CurrentDomain.BaseDirectory;
            //linkLabel1.Visible = false;

            var ia = new Action(StartService).BeginInvoke(null, null);
            while (!ia.IsCompleted)
            {
                Thread.Sleep(300);
            }
        }

        void StartService()
        {
            serviceHost = new ServiceHost(typeof(FilesService), new Uri(baseAddress));
            serviceHost.Open();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var ia = new Action(StartService).BeginInvoke(null, null);
            while (!ia.IsCompleted)
            {
                Thread.Sleep(300);
            }
            button1.Text = "正在运行";
            button2.Text = "停止服务";
            linkLabel1.Text = baseAddress+"help";
            button1.Enabled = false;
            button2.Enabled = true;
            //linkLabel1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                serviceHost.Abort();
                serviceHost.Close();
                button1.Text = "开启服务";
                button2.Text = "服务停止";
                button1.Enabled = true;
                button2.Enabled = false;
                linkLabel1.Visible = false;
            }
            catch { }
        }

        ServiceHost serviceHost = null;

        //已隐藏
        private void button3_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(label2.Text);
            if (di.Exists)
            {
                listView1.Items.Clear();
                listView1.BeginUpdate();
                FilesService.files = di.GetFiles().Select(t =>
                {
                    string mappath = Guid.NewGuid() + "/" + t.Name;
                    listView1.Items.Add(new ListViewItem(new string[] { t.Name, t.Length.ToString(), mappath }));
                    return new CusFileInfo() { filepath = t.FullName, FileName = t.Name, FileLength = t.Length};
                }).ToList();
                listView1.EndUpdate();
            }
        }

        //文件目录
        private void button4_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = AppDomain.CurrentDomain.BaseDirectory;

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    listView1.Items.Clear();
                    listView1.BeginUpdate();
                    listView1.EndUpdate();
                    label2.Text = fbd.SelectedPath;


                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(label2.Text);
                    if (di.Exists)
                    {
                        listView1.Items.Clear();
                        listView1.BeginUpdate();
                        FilesService.files = di.GetFiles().Select(t =>
                        {
                            listView1.Items.Add(new ListViewItem(new string[] { t.Name, t.Length.ToString()}));
                            return new CusFileInfo() { filepath = t.FullName, FileName = t.Name, FileLength = t.Length};
                        }).ToList();
                        listView1.EndUpdate();
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (button1.Enabled)
            {
                MessageBox.Show("请先开启服务。");
                return;
            }
            if (listView1.SelectedItems.Count > 0)
            {
                string url = baseAddress + listView1.SelectedItems[0].SubItems[2].Text;
                Clipboard.SetText(url);
            }
            else
            {
                MessageBox.Show("请先选择一行内容。");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serviceHost.Abort();
            serviceHost.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "文件下载服务_V2.00.001.001_20170327";
        }
    }
}
