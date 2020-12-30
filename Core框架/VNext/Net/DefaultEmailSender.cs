using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VNext.Data;
using VNext.Extensions;
using VNext.Options;

namespace VNext.Net
{
    /// <summary>
    /// 默认邮件发送者
    /// </summary>
    public class DefaultEmailSender : IEmailSender
    {
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 初始化一个<see cref="DefaultEmailSender"/>类型的新实例
        /// </summary>
        public DefaultEmailSender(IServiceProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// 发送Email
        /// </summary>
        /// <param name="email">接收人Email</param>
        /// <param name="subject">Email标题</param>
        /// <param name="body">Email内容</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            VNextOptions options = _provider.GetVNextOptions();
            MailSenderOption mailSender = options.MailSender;
            if (mailSender == null || mailSender.Host == null || mailSender.Host.Contains("请替换"))
            {
                throw new Exception("邮件发送选项不存在，请在appsetting.json配置VNext:MailSender节点");
            }

            string host = mailSender.Host,
                displayName = mailSender.DisplayName,
                userName = mailSender.UserName,
                password = mailSender.Password;
            bool enableSsl = mailSender.EnableSsl;
            int port = mailSender.Port;
            if (port == 0)
            {
                port = enableSsl ? 465 : 25;
            }

            SmtpClient client = new SmtpClient(host, port)
            {
                UseDefaultCredentials = true,
                EnableSsl = enableSsl,
                Credentials = new NetworkCredential(userName, password)
            };

            string fromEmail = userName.Contains("@") ? userName : "{0}@{1}".FormatWith(userName, client.Host.Replace("smtp.", ""));
            MailMessage mail = new MailMessage
            {
                From = new MailAddress(fromEmail, displayName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mail.To.Add(email);
            await client.SendMailAsync(mail);
        }
    }
}