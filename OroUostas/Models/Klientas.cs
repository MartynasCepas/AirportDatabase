using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OroUostas.Models
{
    public class Klientas
    {
        [DisplayName("ID")]
        [Required]
        public int id { get; set; }
        [DisplayName("Asmens kodas")]
        [Required]
        public Int64 asmens_kodas { get; set; }
        [DisplayName("Vardas")]
        [Required]
        public string vardas { get; set; }
        [DisplayName("Pavardė")]
        [Required]
        public string pavarde { get; set; }
        [DisplayName("Amzius")]
        [Required]
        public int amzius { get; set; }
        [DisplayName("Telefonas")]
        [Required]
        public string telefono_numeris { get; set; }
        [DisplayName("Elektroninis paštas")]
        [EmailAddress]
        [Required]
        public string elektroninis_pastas { get; set; }
    }
}