using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OroUostas.Models;

namespace OroUostas.ViewModels
{
    public class BilietasViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Kaina")]
        public int kaina { get; set; }
        [DisplayName("Savininkas")]
        public string savininkas { get; set; }
    }
}