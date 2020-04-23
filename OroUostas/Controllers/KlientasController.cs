using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OroUostas.Models;
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

        // GET: Klientas/Create
        public ActionResult Create()
        {
            Klientas klientas = new Klientas();
            return View(klientas);
        }

        // POST: Klientas/Create
        [HttpPost]
        public ActionResult Create(Klientas collection)
        {
            try
            {
                // Patikrinama ar klientas su tokiu asmens kodu jau egzistuoja
                Klientas tmpKlientas = klientaiRepository.getKlientas(collection.asmens_kodas);
                if (tmpKlientas.asmens_kodas!=0)
                {
                    ModelState.AddModelError("asmens_kodas", "Klientas su tokiu asmens kodu jau užregistruotas");
                    return View(collection);
                }

                collection.id = klientaiRepository.getNewId();
                //Jei nera sukuria nauja klienta
                if (ModelState.IsValid)
                {
                    klientaiRepository.addKlientas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Klientas/Delete/5
        public ActionResult Delete(int id)
        {
            return View(klientaiRepository.getKlientas(id));
        }

        // POST: Klientas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                bool naudojama = false;

           //     if (klientaiRepository.getKlientasSutarciuCount(id)>0)
           //     {
           //         naudojama = true;
           //         ViewBag.naudojama = "Negalima pašalinti klientas turėjo sudarytų sutarčių";
           //         return View(klientasRepository.getKlientas(id));
           //     }

                
                klientaiRepository.deleteKlientas(id);
                

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Klientas/Edit/5
        public ActionResult Edit(int id)
        {
            return View(klientaiRepository.getKlientas(id));
        }

        // POST: Klientas/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Klientas collection)
        {
            try
            {
                // Atnaujina kliento informacija
                if (ModelState.IsValid)
                {
                    klientaiRepository.updateKlientas(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }
    }
}