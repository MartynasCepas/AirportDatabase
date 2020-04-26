using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OroUostas.Reps;
using OroUostas.ViewModels;

namespace OroUostas.Controllers
{
    public class SkrydzioBilietasController : Controller
    {
        SkrydzioBilietasRepository skrydzioBilietasRepository = new SkrydzioBilietasRepository();
        SkrydziaiRepository skrydziaiRepository = new SkrydziaiRepository();
        BilietaiRepository bilietaiRepository = new BilietaiRepository();

        public ActionResult Index()
        {
            return View(skrydzioBilietasRepository.getSkrydzioBilietai());
        }

        public ActionResult Create()
        {
            SkrydzioBilietasEditViewModel bilietas = new SkrydzioBilietasEditViewModel();
            PopulateSelections(bilietas);
            return View(bilietas);
        }

        [HttpPost]
        public ActionResult Create(SkrydzioBilietasEditViewModel collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    collection.id = skrydzioBilietasRepository.getNewId();
                    skrydzioBilietasRepository.addBilietas(collection);
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
            SkrydzioBilietasEditViewModel bilietas = skrydzioBilietasRepository.getBilietas(id);
            PopulateSelections(bilietas);
            return View(bilietas);
        }

        [HttpPost]
        public ActionResult Edit(int id, SkrydzioBilietasEditViewModel collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    skrydzioBilietasRepository.updateBilietas(collection);
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
            SkrydzioBilietasEditViewModel bilietas = skrydzioBilietasRepository.getBilietas(id);
            return View(bilietas);
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                SkrydzioBilietasEditViewModel bilietas = skrydzioBilietasRepository.getBilietas(id);

                skrydzioBilietasRepository.deleteBilietas(id);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public void PopulateSelections(SkrydzioBilietasEditViewModel bilietas)
        {
            var bilietai = bilietaiRepository.getBilietai();
            var skrydziai = skrydziaiRepository.getSkrydziai();

            List<SelectListItem> selectListBilietai = new List<SelectListItem>();
            List<SelectListItem> selectListSkrydziai = new List<SelectListItem>();

            foreach (var item in bilietai)
            {
                selectListBilietai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = Convert.ToString(item.id) });
            }

            foreach (var item in skrydziai)
            {
                selectListSkrydziai.Add(new SelectListItem() { Value = Convert.ToString(item.id), Text = Convert.ToString(item.id) });
            }


            bilietas.BilietasList = selectListBilietai;
            bilietas.SkrydisList = selectListSkrydziai;
        }
    }
}