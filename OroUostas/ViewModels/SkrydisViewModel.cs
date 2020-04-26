using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OroUostas.ViewModels
{
    public class SkrydisViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Data")]
        public string data { get; set; }
        [DisplayName("Lektuvas")]
        public string lektuvas { get; set; }
        [DisplayName("Kryptis")]
        public string kryptis { get; set; }
    }
}