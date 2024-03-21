using System.Linq;
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
        public ActionResult AddProduct(Product product, string Tipo)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Tipo))
                {
                    product.Tipologia = Tipo;
                }

                var selectedCompany = db.Companies.FirstOrDefault(c => c.id == product.SelectedCompanyId);
                if (selectedCompany != null)
                {
                    product.Company = selectedCompany;
                }

                db.Products.Add(product);
                db.SaveChanges();
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
    }
}
