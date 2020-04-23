using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OroUostas.ViewModels
{
    public class BilietasEditViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Kaina")]
        [Required]
        public int kaina { get; set; }
        [DisplayName("Savininkas")]
        [Required]
        public int fk_klientas { get; set; }

        public IList<SelectListItem> KlientasList { get; set; }
    }
}