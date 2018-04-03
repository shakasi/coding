using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace WebSite.Utilities.Mail
{
    /// <summary>
    /// exchange邮件客户端类
    /// </summary>
    public class ExChangeMailClient
    {
        /// <summary>
        /// exchange服务对象
        /// </summary>
        private static ExchangeService _exchangeService = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
        /// <summary>
        /// 获取收件箱
        /// </summary>
        /// <param name="userId">当前用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="domain">域</param>
        /// <param name="pageSize">一次加载的数量</param>
        /// <param name="offset">偏移量</param>
        /// <returns></returns>
        public static List<Email> GetInbox(string url, string userId, string pwd, string domain, int pageSize, int offset)
        {
            try
            {
                if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(pwd) || string.IsNullOrEmpty(domain))
                {
                    throw new ArgumentNullException("当前用户信息为空，无法访问exchange服务器");
                }
                List<Email> lstEmails = new List<Email>();
                _exchangeService.Credentials = new NetworkCredential(userId, pwd, domain);
                _exchangeService.Url = new Uri(url);
                ItemView view = new ItemView(pageSize, offset);
                FindItemsResults<Item> findResults = _exchangeService.FindItems(WellKnownFolderName.Inbox, SetFilter(), view);
                foreach (Item item in findResults.Items)
                {
                    item.Load(PropertySet.FirstClassProperties);
                    //转化为EmailMessage获取 获取邮件详情
                    var currentEmail = (Microsoft.Exchange.WebServices.Data.EmailMessage)(item);
                    List<string> ccRecipientsEmailLists = new List<string>();
                    List<string> bccRecipientsEmailLists = new List<string>();
                    foreach (var cc in currentEmail.CcRecipients)
                    {
                        ccRecipientsEmailLists.Add(cc.Address);
                    }
                    foreach (var bcc in currentEmail.BccRecipients)
                    {
                        bccRecipientsEmailLists.Add(bcc.Address);
                    }
                    lstEmails.Add(new Email()
                    {
                        ExchangeItemId = item.Id.ChangeKey,
                        body = item.Body.Text,
                        Mail_cc = string.Join(";", ccRecipientsEmailLists.ToArray()),
                        Mail_bcc = string.Join(";", bccRecipientsEmailLists.ToArray()),
                        Mail_from = currentEmail.From.Address,
                        IsRead = item.IsNew,
                        Subject = item.Subject,
                        CreateOn = item.DateTimeCreated
                    });

                }
                return lstEmails;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        /// <summary>
        /// 根据用户邮件地址返回用户的未读邮件数
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static int GetUnReadMailCountByUserMailAddress(string url,string userId, string pwd, string domain, string email)
        {
            int unRead = 0;
            try
            {
                _exchangeService.Credentials = new NetworkCredential(userId, pwd, domain);
                _exchangeService.Url = new Uri(url);
                _exchangeService.ImpersonatedUserId = new Microsoft.Exchange.WebServices.Data.ImpersonatedUserId(Microsoft.Exchange.WebServices.Data.ConnectingIdType.SmtpAddress, email);

                unRead = Microsoft.Exchange.WebServices.Data.Folder.Bind(_exchangeService, Microsoft.Exchange.WebServices.Data.WellKnownFolderName.Inbox).UnreadCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return unRead;
        }
        /// <summary>
        /// 过滤器
        /// </summary>
        /// <returns></returns>
        private static SearchFilter SetFilter()
        {
            List<SearchFilter> searchFilterCollection = new List<SearchFilter>();
            //searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, false));
            //searchFilterCollection.Add(new SearchFilter.IsEqualTo(EmailMessageSchema.IsRead, true));
            //筛选今天的邮件
            SearchFilter start = new SearchFilter.IsGreaterThanOrEqualTo(EmailMessageSchema.DateTimeCreated, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")));
            SearchFilter end = new SearchFilter.IsLessThanOrEqualTo(EmailMessageSchema.DateTimeCreated, Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 23:59:59")));
            searchFilterCollection.Add(start);
            searchFilterCollection.Add(end);
            SearchFilter filter = new SearchFilter.SearchFilterCollection(LogicalOperator.And, searchFilterCollection.ToArray());
            return filter;
        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static void SendMail(string url,Email email, string userId, string pwd, string domain)
        {
            try
            {
                _exchangeService.Credentials = new NetworkCredential(userId, pwd, domain);
                _exchangeService.Url = new Uri(url);
                //发送人
                Mailbox mail = new Mailbox(email.Mail_from);
                //邮件内容
                EmailMessage message = new EmailMessage(_exchangeService);
                string[] strTos = email.Mail_to.Split(';');
                //接收人
                foreach (string item in strTos)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        message.ToRecipients.Add(item);
                    }
                }

                //抄送人
                //foreach (string item in email.Mail_cc.Split(';'))
                //{
                //    if (!string.IsNullOrEmpty(item))
                //    {
                //        message.CcRecipients.Add(item);
                //    }

                //}

                //附件
                byte[] bytes = File.ReadAllBytes(@"D:\CommonConfig.xml");
                Stream stream = new MemoryStream(bytes);
                //Attachment at = new Attachment(stream, "CommonConfig.xml");
                message.Attachments.AddFileAttachment("CommonConfig.xml", stream);

                //邮件标题
                message.Subject = email.Subject;
                //邮件内容
                message.Body = new MessageBody(email.body);
                //发送并且保存
                message.SendAndSaveCopy();

            }
            catch (Exception ex)
            {
                throw new Exception("发送邮件出错，" + ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }

    public class Email
    {
        public string Subject { get; set; }

        public string body { get; set; }

        public string Mail_cc { get; set; }

        public string Mail_from { get; set; }

        public string Mail_to { get; set; }

        public string ExchangeItemId { get; set; }

        public string Mail_bcc { get; set; }

        public bool IsRead { get; set; }

        public string DateTimeCreated { get; set; }

        public DateTime CreateOn { get; set; }
    }
}