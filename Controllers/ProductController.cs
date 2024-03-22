using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    [Authorize(Roles = "farmacista")]
    public class ProductController : Controller
    {
        private VetContext db = new VetContext();

        public ActionResult Index()
        {
            var companies = db.Companies.Include(c => c.Product).ToList();
            return View(companies);
        }

        public ActionResult AddProduct()
        {
            var companies = db.Companies.ToList();
            var usages = db.Usages.ToList();

            var companyOptions = companies.Select(c => new SelectListItem
            {
                Text = c.Nome,
                Value = c.id.ToString()
            }).ToList();

            ViewBag.CompanyOptions = companyOptions;
            ViewBag.UsageOptions = new SelectList(usages, "Id", "Utilizzo");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product, int UsageId)
        {
            if (ModelState.IsValid)
            {
                var selectedCompany = db.Companies.FirstOrDefault(c => c.id == product.SelectedCompanyId);
                if (selectedCompany != null)
                {
                    product.Company = selectedCompany;
                }

                db.Products.Add(product);
                db.SaveChanges();

                var usage = db.Usages.Find(UsageId);
                if (usage != null)
                {
                    var container = GetContainerByUsage(usage);

                    var codiceCassetto = $"{container.CodiceArmadietto}-{usage.Utilizzo}";
                    var newBox = new Box
                    {
                        CodiceCassetto = codiceCassetto,
                        ContainerId = container.id,
                        UsageId = usage.Id,
                        Product = product
                    };

                    db.Boxes.Add(newBox);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

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

        private Container GetContainerByUsage(Usage usage)
        {
            switch (usage.Utilizzo)
            {
                case "Vitamine":
                case "Minerali":
                case "Integratori alimentari":
                    return db.Containers.FirstOrDefault(c => c.CodiceArmadietto == "Alimenti");
                case "Diuretici":
                case "Antidolorifici":
                case "Lassativi":
                case "Analgesici":
                case "Antimicotici":
                case "Disinfettanti":
                case "Antiparassitari":
                case "Emollienti cutanei":
                    return db.Containers.FirstOrDefault(c => c.CodiceArmadietto == "Farmaci da banco");
                default:
                    return db.Containers.FirstOrDefault(c => c.CodiceArmadietto == "Farmaci con ricetta");
            }
        }
    }
}