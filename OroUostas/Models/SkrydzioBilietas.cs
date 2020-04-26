using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OroUostas.Models
{
    public class SkrydzioBilietas
    {
        [DisplayName("ID")]
        public int id { get; set; }
        public virtual Bilietas bilietas { get; set; }
        public virtual Skrydis skrydis { get; set; }
    }
}