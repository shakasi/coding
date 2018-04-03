using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shaka.Model
{
    public class ConnInfo
    {
        private string _ip = "127.0.0.1";
        public string Ip
        {
            get { return this._ip; }
            set { this._ip = value; }
        }

        public string Name { get; set; }
        public string UserId { get; set; }
        public string Pwd { get; set; }
    }
}
