using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OroUostas.ViewModels
{
    public class SkrydisEditViewModel
    {
        [DisplayName("ID")]
        public int id { get; set; }
        [DataType(DataType.DateTime)]
        [Required]
        [DisplayName("Skrydzio data")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime data { get; set; }
        [DisplayName("Laikas")]
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{hh:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan laikas { get; set; }
        [DisplayName("Lektuvas")]
        [Required]
        public int fk_lektuvas { get; set; }
        [DisplayName("Kryptis")]
        [Required]
        public int fk_kryptis { get; set; }

        public IList<SelectListItem> LektuvasList { get; set; }
        public IList<SelectListItem> KryptisList { get; set; }
    }
}