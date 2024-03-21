using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
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
            vm.admission.animalID = animalID;
            if (ModelState.IsValid)
            {
                db.Admissions.Add(admission);
                db.SaveChanges();
                TempData["success"] = "true";

            }
            else
            {
                TempData["error"] = "true";

            }
            return RedirectToAction("Animals", "Vet");
        }
        public ActionResult Ricoveri()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Tutti", Value = "1" });
            items.Add(new SelectListItem { Text = "Mensili", Value = "2" });
            items.Add(new SelectListItem { Text = "Attivi", Value = "3" });

            ViewBag.options = items;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAnimal(AnimalsViewModel vm, HttpPostedFileBase FileFoto)
        {
            if (ModelState.IsValid)
            {
                Animal animal = vm.animal;
                if (FileFoto != null && FileFoto.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(FileFoto.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    FileFoto.SaveAs(path);
                    animal.FileFoto = fileName;
                }
                db.Animals.Add(animal);
                db.SaveChanges();
                TempData["success"] = "true";
            }
            else
            {
                TempData["error"] = "true";
            }



            return RedirectToAction("Animals", "Vet");
        }


        [HttpPost]
        public ActionResult EditAnimal(AnimalsViewModel vm, int animalId, HttpPostedFileBase FileFoto)
        {
            var animal = db.Animals.Where(a => a.id == animalId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                TempData["foto"] = FileFoto;

                if (FileFoto != null && FileFoto.ContentLength > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(FileFoto.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    FileFoto.SaveAs(path);
                    animal.FileFoto = fileName;
                }

                animal.Name = vm.animal.Name;
                animal.NomeProprietario = vm.animal.NomeProprietario;
                animal.CognomeProprietario = vm.animal.CognomeProprietario;
                animal.Colore = vm.animal.Colore;
                animal.DataNascita = vm.animal.DataNascita;
                animal.Tipologia = vm.animal.Tipologia;
                animal.IdMicrochip = vm.animal.IdMicrochip;
                db.Entry(animal).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "true";
            }
            else
            {
                TempData["error"] = "true";
            }

            return RedirectToAction("Animals");
        }

        [HttpPost]
        public ActionResult EditAdmission([Bind(Include = "id, animalID, DataInizio, DataFine")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(admission).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "true";
                return RedirectToAction("Ricoveri", "Vet");
            }
            else
            {
                TempData["error"] = "true";
            }
            return View(admission);
        }

        public ActionResult EditAdmission(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Admission ricovero = db.Admissions.Find(id);
            if (ricovero == null)
            {
                return HttpNotFound();
            }
            return View(ricovero);
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
                              DataInizio = ad.DataInizio,
                              DataFine = ad.DataFine,
                              Nome = al.Name,
                              Tipologia = al.Tipologia,
                              Colore = al.Colore,
                              DataReg = al.DataReg,
                              DataNascita = al.DataNascita,
                              Microchip = al.IdMicrochip,
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AllAdmissions()
        {
            var result = (from ad in db.Admissions
                          join al in db.Animals on ad.animalID equals al.id
                          select new
                          {
                              admissionId = ad.id,
                              DataInizio = ad.DataInizio,
                              DataFine = ad.DataFine,
                              Nome = al.Name,
                              Tipologia = al.Tipologia,
                              Colore = al.Colore,
                              DataReg = al.DataReg,
                              DataNascita = al.DataNascita,
                              Microchip = al.IdMicrochip,
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public JsonResult Active()
        {
            var min = DateTime.MinValue;
            var result = (from ad in db.Admissions
                          join al in db.Animals on ad.animalID equals al.id
                          where (ad.DataFine == null)
                          select new
                          {
                              admissionId = ad.id,
                              DataInizio = ad.DataInizio,
                              DataFine = ad.DataFine,
                              Nome = al.Name,
                              Tipologia = al.Tipologia,
                              Colore = al.Colore,
                              DataReg = al.DataReg,
                              DataNascita = al.DataNascita,
                              Microchip = al.IdMicrochip,
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MarkAsEnded(int id)
        {
            var admission = db.Admissions.Where(a => a.id == id).FirstOrDefault();
            admission.DataFine = DateTime.Now;
            db.Entry(admission).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Ricoveri", "Vet");
        }


    }
}