using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Net.Mail;
using log4net;
using Cuscapi.Utils;
using Cuscapi.Model;

namespace Cuscapi.BLL
{
    public class EmailBLL
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private XmlHelper _xmlHelper = null;
        private SqlHelper _sqlHelper = null;

        private string _currentPath = null;

        public EmailBLL()
        {
            _currentPath = GetType().Assembly.Location;
            _currentPath = _currentPath.Substring(0, _currentPath.LastIndexOf(@"\")) + "\\";
            string mailConfigPath = _currentPath + @"ConfigFiles\MailConfig.xml";

            string connFile = _currentPath + @"ConfigFiles\CommonConfig.xml";
            string connStr = new XmlHelper(connFile).GetValue("ConnectionString");
            _sqlHelper = new SqlHelper(connStr);

            _xmlHelper = new XmlHelper(mailConfigPath);
        }

        public EmailInfo GetEntityFromXml()
        {
            return _xmlHelper.Reader<EmailInfo>("Root", "Value");
        }

        public bool SaveEmailInfo(EmailInfo emailInfo)
        {
            return _xmlHelper.WriteXml<EmailInfo>(emailInfo, "Value");
        }

        public void SendEmail()
        {
            SendEmail(null);
        }

        public void SendEmail(EmailInfo emailInfo)
        {
            if (emailInfo == null)
            {
                emailInfo = GetEntityFromXml();
                if (emailInfo == null)
                {
                    string err = "emailInfo is null";
                    _log.Fatal(err);
                }
            }

            try
            {
                _sqlHelper.BeginTran();
                #region 额外由数据库表生成的附件
                var attList = _sqlHelper.Reader<AttachmentInfo>(@"select ID,storenum,childName,childGender,childAge,parentPhone,reachAddress,curriculumName
                    from tmpDTTalentAcademy where ISNULL(IsSendEmail,0)<>1");

                _sqlHelper.ExecuteNonQuery("update tmpDTTalentAcademy set IsSendEmail=1");

                if (attList.Count > 0)
                {
                    string attFileRemark = _currentPath + "说明.txt";
                    string attFile = _currentPath + "CustomerCallBack.xlsx";

                    //ExcelHelper.SaveDataToExcel<AttachmentInfo>(attList, attFile);
                    ExcelAsposeHelper.OutFileToDisk<AttachmentInfo>(attList, "客户反馈", attFile);

                    System.Threading.Thread.Sleep(200);
                    emailInfo.AttachmentList.Add(attFileRemark);
                    emailInfo.AttachmentList.Add(attFile);

                }
                #endregion

                EmailHelper emailHelper = new EmailHelper(emailInfo.SendServer, emailInfo.UserName, emailInfo.Password,
                         emailInfo.DisplayName, emailInfo.ToName, emailInfo.ToEmail, emailInfo.ToSubject, emailInfo.AttachmentList, emailInfo.ToBody);

                _log.Info(emailInfo.ToEmail);
                emailHelper.SendEmail();
                _sqlHelper.CommitTran();
                _log.Info("邮件发送成功！");
             }
            catch (Exception ex)
            {       
                _log.Fatal(ex);
                _sqlHelper.RollBackTran();
            }
        }
    }
}