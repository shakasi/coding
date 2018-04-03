using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSite.Utilities.Mail;

namespace ExchangeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //string email = "shakasi@cuscapi.com";
            string url = "https://mail.cuscapi.com/ews/exchange.asmx";

            Email email = new Email();
            email.Subject = "昨天项目怎么样了啊";
            email.body = "昨天开会开的如何了";
            email.Mail_from = "shakasi@cuscapi.com";
            email.Mail_to = "shakasi@cuscapi.com";
            //email.ExchangeItemId = "昨天开会开的如何了";
            //email.Mail_bcc = "shakasi@cuscapi.com";

            string userId = "shakasi";
            string pwd = "@Cuscapi2015";
            string domain = "cuscapi";
            try
            {
                ExChangeMailClient.SendMail(url,email, userId,pwd,domain);
                //ExChangeMailClient.GetInbox(url,userId, pwd, domain, 1, 1);
            }
            catch (Exception ex)
            {

            }
        }
    }
}