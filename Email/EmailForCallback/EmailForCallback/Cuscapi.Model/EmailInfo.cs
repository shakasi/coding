using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cuscapi.Model
{
    public class EmailInfo
    {
        public string SendServer { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string DisplayName { get; set; }
        public string ToName { get; set; }
        public string ToEmail { get; set; }
        public string ToSubject { get; set; }
        public string ToBody { get; set; }

        private List<string> _attachmentList = new List<string>();
        public List<string> AttachmentList
        {
            get { return this._attachmentList; }
            set { this._attachmentList = value; }
        }
    }
}
