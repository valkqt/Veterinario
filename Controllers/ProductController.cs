using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    [Authorize(Roles = "farmacista")]
    public class ProductController : Controller
    {
        private VetContext db = new VetContext();


        public ActionResult Index()
        {
            var companies = db.Companies.ToList();
            return View(companies);
        }

        public ActionResult AddProduct()
        {
            ViewBag.Companies = db.Companies.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, string Tipo, int CompanyId)
        {
            if (ModelState.IsValid)
            {
                if (CompanyId > 0)
                {
                    var company = db.Companies.Find(CompanyId);

                    if (company != null)
                    {
                        product.Company = company;

                        if (!string.IsNullOrEmpty(Tipo))
                        {
                            product.Tipologia = Tipo;
                        }

                        db.Products.Add(product);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "La compagnia selezionata non esiste.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Seleziona una compagnia valida.");
                }
            }

            ViewBag.Companies = db.Companies.ToList();
            return View(product);
        }



        [HttpPost]
        public ActionResult IncreaseQuantity(int productId)
        {
            var product = db.Products.Find(productId);
            if (product != null)
            {
                product.Quantità++;

                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}