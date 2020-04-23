using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OroUostas.Reps;
using OroUostas.ViewModels;

namespace OroUostas.Controllers
{
    public class BilietasController : Controller
    {
        KlientaiRepository klientaiRepository = new KlientaiRepository();
        BilietaiRepository bilietaiRepository = new BilietaiRepository();

        public ActionResult Index()
        {
            return View(bilietaiRepository.getBilietai());
        }

        public ActionResult Create()
        {
            BilietasEditViewModel bilietas = new BilietasEditViewModel();
            PopulateSelections(bilietas);
            return View(bilietas);
        }

        [HttpPost]
        public ActionResult Create(BilietasEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    collection.id = bilietaiRepository.getNewId();
                    bilietaiRepository.addBilietas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Edit(int id)
        {
            BilietasEditViewModel bilietas = bilietaiRepository.getBilietas(id);
            PopulateSelections(bilietas);
            return View(bilietas);
        }

        [HttpPost]
        public ActionResult Edit(int id, BilietasEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bilietaiRepository.updateBilietas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        public ActionResult Delete(int id)
        {
            BilietasEditViewModel bilietas = bilietaiRepository.getBilietas(id);
            return View(bilietas);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                BilietasEditViewModel bilietas = bilietaiRepository.getBilietas(id);
         //       bool naudojama = false;

         //       if (modeliuRepository.getModelisCount(id)>0)
         //       {
         //           naudojama = true;
         //           ViewBag.naudojama = "Negalima pašalinti modelio, yra sukurtų automobilių su šiuo modeliu.";
         //           return View(modelis);
         //       }

                
                bilietaiRepository.deleteBilietas(id);
                
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(BilietasEditViewModel bilietas)
        {
            var klientai = klientaiRepository.getKlientai();
            List<SelectListItem> selectListKlientai = new List<SelectListItem>();

            foreach (var item in klientai)
            {
                selectListKlientai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = (item.asmens_kodas + " " + item.vardas + " " + item.pavarde) });
            }

            bilietas.KlientasList = selectListKlientai;
        }
    }
}