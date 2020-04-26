using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OroUostas.Models
{
    public class Lektuvas
    {
        public int id { get; set; }
        [DisplayName("Modelis")]
        [Required]
        public string modelis { get; set; }
        [DisplayName("Kategorija")]
        [Required]
        public string kategorija { get; set; }
        [DisplayName("Vietu Skaicius")]
        [Required]
        public int vietuSkaicius { get; set; }
        [DisplayName("Svoris")]
        [Required]
        public int svoris { get; set; }
        [DisplayName("Pagaminimo Metai")]
        [Required]
        public int pagaminimoMetai { get; set; }
    }
}