using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OroUostas.ViewModels
{
    public class SkrydziaiEditViewModel
    {
        public virtual IEnumerable<SkrydisEditViewModel> skrydziai { get; set; }
    }
}