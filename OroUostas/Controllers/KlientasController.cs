using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OroUostas.Reps;

namespace OroUostas.Controllers
{
    public class KlientasController : Controller
    {
        KlientaiRepository klientaiRepository = new KlientaiRepository();
        public ActionResult Index()
        {
            return View(klientaiRepository.getKlientai());
        }
    }
}