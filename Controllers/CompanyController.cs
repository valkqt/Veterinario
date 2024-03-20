using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    public class CompanyController : Controller
    {
        private VetContext db = new VetContext();

        public ActionResult AddCompany()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCompany(Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index", "Product");
            }

            return View(company);
        }
    }
}