using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OroUostas.ViewModels
{
    public class SkrydzioBilietasEditViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DisplayName("Bilietas")]
        [Required]
        public int fk_bilietas { get; set; }
        [DisplayName("Skrydis")]
        [Required]
        public int fk_skrydis { get; set; }

        public IList<SelectListItem> BilietasList { get; set; }
        public IList<SelectListItem> SkrydisList { get; set; }
    }
}