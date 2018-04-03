using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.Configuration;
using System.IO;
using FilesContract.Demo;

namespace FilesClient.Demo
{
    public partial class Form1 : Form
    {
        SaveFileDialog sf = new SaveFileDialog();
        public Form1()
        {
            InitializeComponent();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings.AllKeys.Contains("BaseAddress"))
            {
                DataManager.BaseAddress = config.AppSettings.Settings["BaseAddress"].Value;
            }
            //textBox1.Text = DataManager.BaseAddress;
        }
        /// <summary>
        /// 获取下载列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != DataManager.BaseAddress)
            {
                DataManager.BaseAddress = textBox1.Text;
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings.Remove("BaseAddress");
                config.AppSettings.Settings.Add("BaseAddress", textBox1.Text);
                config.Save();
            }
            try
            {
                DataManager.Channel.GetFiles().ForEach(t => { listView1.Items.Add(new ListViewItem(new string[] { t.FileName, t.FileLength.ToString() })); });
                textBox1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 下载选中文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    if (listView1.SelectedItems.Count > 0)
        //    {
        //        for (int i = 0; i < listView1.SelectedItems.Count; i++)
        //        {
        //            var tmp = listView1.SelectedItems[i].SubItems[0].Text;
        //            sf.FileName = tmp;
        //            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //            {
        //                Form2 f = new Form2(sf.FileName, tmp, long.Parse(listView1.SelectedItems[0].SubItems[1].Text));
        //                f.StartPosition = FormStartPosition.CenterParent;
        //                f.ShowDialog();
        //                f.Dispose();
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("请先选择一行内容。");
        //    }
        //}


        private void button2_Click(object sender, EventArgs e)
        {
            string fileName = textBox1.Text;
            sf.FileName = fileName;

            if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Form2 f = new Form2(sf.FileName, fileName, 0);
                f.StartPosition = FormStartPosition.CenterParent;
                f.ShowDialog();
                f.Dispose();
            }
        }


        /// <summary>
        /// 复制选中链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                string url = DataManager.BaseAddress + listView1.SelectedItems[0].SubItems[2].Text;
                try
                {
                    Clipboard.SetText(url);
                }
                catch { }
            }
            else
            {
                MessageBox.Show("请先选择一行内容。");
            }
        }

        /// <summary>
        /// 打开HTTP下载窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
        }
    }
}
