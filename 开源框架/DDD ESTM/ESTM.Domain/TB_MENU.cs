//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ESTM.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_MENU
    {
        public TB_MENU()
        {
            this.TB_MENUROLE = new HashSet<TB_MENUROLE>();
        }
    
        public string MENU_ID { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_URL { get; set; }
        public string PARENT_ID { get; set; }
        public string MENU_LEVEL { get; set; }
        public string SORT_ORDER { get; set; }
        public string STATUS { get; set; }
        public string REMARK { get; set; }
        public string MENU_ICO { get; set; }
    
        public virtual ICollection<TB_MENUROLE> TB_MENUROLE { get; set; }
    }
}
