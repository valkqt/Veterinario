using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    public class VetController : Controller
    {
        // GET: Vet
        private VetContext db = new VetContext();

        public ActionResult Animals()
        {
            return View(db.Animals.ToList());
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
    }
}