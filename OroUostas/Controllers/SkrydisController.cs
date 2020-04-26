using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OroUostas.Models;
using OroUostas.Reps;
using OroUostas.ViewModels;

namespace OroUostas.Controllers
{
    public class SkrydisController : Controller
    {
        SkrydziaiRepository skrydziaiRepository = new SkrydziaiRepository();
        LektuvaiRepository lektuvaiRepository = new LektuvaiRepository();
        KryptysRepository kryptysRepository = new KryptysRepository();

        public ActionResult Index()
        {
            return View(skrydziaiRepository.getSkrydziai());
        }

        public ActionResult Create()
        {
            SkrydisEditViewModel skrydis = new SkrydisEditViewModel();
            PopulateSelections(skrydis);
            return View(skrydis);
        }

        [HttpPost]
        public ActionResult Create(SkrydisEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    collection.id = skrydziaiRepository.getNewId();
                    skrydziaiRepository.addSkrydis(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public void PopulateSelections(SkrydisEditViewModel skrydis)
        {
            var lektuvai = lektuvaiRepository.getLektuvai();
            var kryptys = kryptysRepository.getKryptys();

            List<SelectListItem> selectListLektuvai = new List<SelectListItem>();
            List<SelectListItem> selectListKryptys = new List<SelectListItem>();

            foreach (var item in lektuvai)
            {
                selectListLektuvai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = (item.id.ToString() + " " + item.modelis) });
            }

            foreach (var item in kryptys)
            {
                selectListKryptys.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = "is: " + item.isOroUosto + " i: "+ item.iOroUosta });
            }

            skrydis.KryptisList = selectListKryptys;
            skrydis.LektuvasList = selectListLektuvai;
        }
    }
}