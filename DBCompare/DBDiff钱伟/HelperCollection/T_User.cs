using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.HelperCollection.DataAccess;
using Utility.HelperCollection.DataAccess.Attribute;

namespace Utility.HelperCollection
{
   // [SqlTable("dbo.T_Users")]
    [SqlTable("T_Users")]
    [SqlPrimaryKey(Fields.ID)]
    public class T_User : EntityBase
    {
        public sealed class Fields
        {
            public const string ID = "ID";
            public const string UserName = "UserName";
            public const string Password = "Password";
            public const string State = "State";
            public const string ConnectionID = "ConnectionID";
            public const string DisplayName = "DisplayName";

        }
        [SqlColumn(Fields.ID)]
        public virtual string ID { get; set; }
        [SqlColumn(Fields.UserName)]
        public virtual string UserName { get; set; }
        [SqlColumn(Fields.Password)]
        public virtual string Password { get; set; }
        [SqlColumn(Fields.State)]
        public virtual int State { get; set; }
        [SqlColumn(Fields.ConnectionID)]
        public virtual string ConnectionID { get; set; }
        [SqlColumn(Fields.DisplayName)]
        public virtual string DisplayName { get; set; }
    }
}
