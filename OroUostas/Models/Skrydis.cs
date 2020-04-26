using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OroUostas.Models
{
    public class Skrydis
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Data")]
        public DateTime data { get; set; }
        [DisplayName("Laikas")]
        public DateTime laikas { get; set; }
        public virtual Lektuvas lektuvas { get; set; }
        public virtual Kryptis kryptis { get; set; }
    }
}