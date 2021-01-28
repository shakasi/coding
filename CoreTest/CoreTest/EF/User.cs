using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.EF
{
    public class User
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public DateTime CreateTime { set; get; } = DateTime.Now;
        public DateTime ModifyTime { set; get; } = DateTime.Now;
    }
}
