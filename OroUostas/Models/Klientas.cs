using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OroUostas.Models
{
    public class Klientas
    {
        public int id { get; set; }
        public Int64 asmens_kodas { get; set; }
        public string vardas { get; set; }
        public string pavarde { get; set; }
        public int amzius { get; set; }
        public string telefono_numeris { get; set; }
        public string elektroninis_pastas { get; set; }
    }
}