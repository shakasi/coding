using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cuscapi.Model
{
    public class TmpDTTalentAcademy
    {
        public int ID { get; set; }
        public string storenum { get; set; }
        public string childName { get; set; }
        public string childGender { get; set; }
        public string childAge { get; set; }
        public string parentPhone { get; set; }
        public string reachAddress { get; set; }
        public string curriculumName { get; set; }
        public int IsSendEmail { get; set; }
        public string distance { get; set; }
        public string submitTime { get; set; }
        public int chk_seq { get; set; }
    }
}