using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cuscapi.Model;

using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.IO;

namespace Cuscapi.Utils
{
    public class EmailHelper
    {
        private string _emailProtocol = string.Empty;
        private string _fromEmail = string.Empty;
        private string _userName = string.Empty;
        private string _passWord = string.Empty;
        private string _displayName = string.Empty;
        private string _toName = string.Empty;
        private string _toEmail = string.Empty;
        private string _toSubject = string.Empty;
        private List<FileInfo> _attachmentList = null;
        private string _toBody = string.Empty;

        public EmailHelper(string emailProtocol, string userName, string passWord, string displayName, string toName
            , string toEmail, string toSubject, List<string> attachmentList, string toBody)
        {
            _emailProtocol = emailProtocol;
            if (emailProtocol == EmailProtocolInfo.Smtp163)
            {
                _fromEmail = userName + "@163.com";
            }
            else if (emailProtocol == EmailProtocolInfo.SmtpGmail)
            {
                _fromEmail = userName + "@gmail.com";
            }
            else if (emailProtocol == EmailProtocolInfo.SmtpQQ)
            {
                _fromEmail = userName + "@qq.com";
            }
            _userName = userName;
            _passWord = passWord;
            _displayName = displayName;
            _toName = toName;
            _toEmail = toEmail;
            _toSubject = toSubject;
            List<FileInfo> fileList = new List<FileInfo>();
            if (attachmentList!=null&& attachmentList.Count>0)
            {
                foreach (string file in attachmentList)
                {
                    //判断文件是否存在
                    if (File.Exists(file))
                    {
                        fileList.Add(new FileInfo(file));
                    }
                    else
                    {
                        throw new Exception("附件文件:" + file + " 未找到！");
                    }
                }
            }      
            _attachmentList = fileList;
            _toBody = toBody;
        }

        public void SendEmail()
        {
            try
            {
                //设置发件人信箱,及显示名字 
                MailAddress from = new MailAddress(_fromEmail, _displayName);
                //设置收件人信箱,及显示名字 
                MailAddress to = new MailAddress(_toEmail, _toName);
                //创建一个MailMessage对象 
                MailMessage oMail = new MailMessage(from, to);
                oMail.Subject = _toSubject; //邮件标题 
                oMail.Body = _toBody; //邮件内容 
                oMail.IsBodyHtml = true; //指定邮件格式,支持HTML格式 

                foreach (FileInfo file in _attachmentList)
                {
                    byte[] bytes = File.ReadAllBytes(file.FullName);
                    Stream stream = new MemoryStream(bytes);
                    Attachment at = new Attachment(stream, file.Name);
                    oMail.Attachments.Add(at);
                }

                oMail.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");//邮件采用的编码 
                oMail.Priority = MailPriority.High;//设置邮件的优先级为高 
                                                   //发送邮件服务器 
                SmtpClient client = new SmtpClient();
                client.Host = _emailProtocol; //指定邮件服务器 
                client.Credentials = new NetworkCredential(_userName, _passWord);//指定服务器邮件,及密码发送 
                client.Send(oMail); //发送邮件 
                oMail.Dispose(); //释放资源 
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}