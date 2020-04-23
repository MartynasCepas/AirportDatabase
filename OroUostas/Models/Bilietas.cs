using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OroUostas.Models
{
    public class Bilietas
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
        [DisplayName("Kaina")]
        [Required]
        public int kaina { get; set; }
        [DisplayName("Savininkas")]
        public virtual Klientas klientas { get; set; }
    }
}