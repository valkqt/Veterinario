using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    [Authorize(Roles = "veterinario")]
    public class VetController : Controller
    {
        // GET: Vet
        private VetContext db = new VetContext();

        public ActionResult Animals()
        {
            ViewBag.Animals = db.Animals.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult Therapy(int? id)
        {
            var animal = db.Animals.Find(id);
            if (animal == null)
            {
                return HttpNotFound();
            }

            if (animal.Therapy == null)
            {
                animal.Therapy = db.Therapies.Where(t => t.Animal.id == id).ToList();
            }

            var therapies = animal.Therapy.OrderByDescending(t => t.DataCura).ToList();

            var viewModel = new AnimalTherapyViewModel
            {
                Animal = animal,
                Therapies = therapies
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult NewTherapy(int animalId, Therapy therapy)
        {
            if (ModelState.IsValid)
            {
                var animal = db.Animals.Find(animalId);
                if (animal == null)
                {
                    return HttpNotFound();
                }
                therapy.Animal = animal;

                db.Therapies.Add(therapy);
                db.SaveChanges();

                return RedirectToAction("Therapy", new { id = animalId });
            }
            else
            {
                var errors = new List<ModelError>();
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        errors.Add(error);
                    }
                }
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
            }

            var animalForViewBag = db.Animals.Find(animalId);
            if (animalForViewBag == null)
            {
                return HttpNotFound();
            }

            var viewModel = new AnimalTherapyViewModel
            {
                Animal = animalForViewBag,
                Therapies = animalForViewBag.Therapy.OrderByDescending(t => t.DataCura).ToList()
            };

            return View("Therapy", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ricovero(AnimalsViewModel vm, int animalID)
        {

            Admission admission = vm.admission;
            admission.animalID = animalID;

            db.Admissions.Add(admission);
            db.SaveChanges();


            return RedirectToAction("Animals");
        }

        [HttpPost]
        public ActionResult AddAnimal(AnimalsViewModel vm)
        {
            Animal animal = vm.animal;
            TempData["error"] = "pepe";
            db.Animals.Add(animal);
            db.SaveChanges();

            return RedirectToAction("Animals");
        }

        public ActionResult Mensili()
        {
            return View();
        }

        public JsonResult Monthly()
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var result = (from ad in db.Admissions
                          join al in db.Animals on ad.animalID equals al.id
                          where (ad.DataInizio >= lastMonth)
                          select new
                          {
                              admissionId = ad.id,
                              StartDate = ad.DataInizio,
                              Nome = al.Name,
                              Tipologia = al.Tipologia,
                              Colore = al.Colore,
                              DataReg = al.DataReg,
                              DataNascita = al.DataNascita,
                              Microchip = al.IdMicrochip,
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);


        }
    }
}