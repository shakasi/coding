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
using System.Text.RegularExpressions;

namespace FilesClient.Demo
{
    public partial class Form3 : Form
    {
        string _savefile;
        int buffersize = 1024;
        long position;
        bool stop;
        SaveFileDialog sf = new SaveFileDialog();
        IAsyncResult downAsync;
        public Form3()
        {
            InitializeComponent();
            button1.Text = "开始";
            button2.Text = "取消";
            button3.Text = "关闭";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (button1.Text == "暂停")
            {
                stop = true;
                while (!downAsync.IsCompleted)
                {
                    Thread.Sleep(300);
                }
                downAsync = null;
                button1.Text = "继续";
            }
            else
            {
                stop = false;
                if (button1.Text == "开始")
                {
                    sf.FileName = textBox1.Text.Substring(textBox1.Text.LastIndexOf('/') + 1);
                    if (sf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        _savefile = sf.FileName;
                    }
                    else
                        goto END;
                }
                downAsync = new Action(Download).BeginInvoke(null, null);
                button1.Text = "暂停";
            }
        END: button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (downAsync != null && !downAsync.IsCompleted)
            {
                if (MessageBox.Show("确定要取消下载吗？", "确认！", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    stop = true;
                    while (!downAsync.IsCompleted)
                    {
                        Thread.Sleep(300);
                    }
                    if (File.Exists(_savefile))
                    {
                        File.Delete(_savefile);
                        button1.Text = "开始";
                    }
                    position = 0;
                    downAsync = null;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(null, null);
            this.Close();
        }

        void Download()
        {
            HttpWebResponse response = null;
            try
            {
                //var fs = new FileStream(_savefile, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                var fs = new FileStream(_savefile, FileMode.Open, FileAccess.Read, FileShare.Read);
                fs.Position = position;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(textBox1.Text);
                request.Method = "GET";
                if (position != 0)
                    request.AddRange((int)position);

                response = (HttpWebResponse)request.GetResponse();

                if (position != 0)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        MessageBox.Show("服务器不支持断点续传，将重新开始下载");
                        position = 0;
                        fs.Position = 0;
                    }
                    else if (response.StatusCode != HttpStatusCode.PartialContent)
                    {
                        response.GetType().GetMethod("Abort", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(response, null);
                        MessageBox.Show("下载请求失败，请检查地址是否错误");
                        DataManager.SetControlText(button1, "开始");
                        return;
                    }
                }
                else if (response.StatusCode != HttpStatusCode.OK)
                {
                    response.GetType().GetMethod("Abort", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(response, null);
                    MessageBox.Show("下载请求失败，请检查地址是否错误");
                    DataManager.SetControlText(button1, "开始");
                    return;
                }
                using (Stream responseStream = response.GetResponseStream())
                {
                    CopyStream(responseStream, fs);
                    response.GetType().GetMethod("Abort", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(response, null);
                }
            }
            catch (WebException ex)
            {
                var errResp = ex.Response as HttpWebResponse;
                using (var stream = errResp.GetResponseStream())
                {
                    string message = null;
                    using (var sr = new StreamReader(stream))
                    {
                        message = sr.ReadToEnd();
                        var match = Regex.Match(message, "(?is)(?<=<Text[^>]*>).*(?=</Text>)");
                        if (match.Success)
                        {
                            message = match.Value;
                        }
                    }
                    MessageBox.Show(message);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
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
                        DataManager.SetControlText(label2, "已接收" + tostream.Position + "字节");
                    }
                    if (stop)
                    {
                        return;
                    }
                }
                if (offset > 0)
                    tostream.Write(buffer, 0, offset);
                DataManager.SetControlText(label2, "下载完成，共接收" + tostream.Position + "字节");
                DataManager.SetControlText(button1, "开始");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                position = tostream.Position;
                tostream.Close();
            }
        }        
    }
}
