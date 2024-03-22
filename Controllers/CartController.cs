using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    public class CartController : Controller
    {
        private VetContext db = new VetContext();

        public ActionResult Index(string searchString)
        {
            var products = db.Products.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Nome.Contains(searchString));
            }

            return View(products.ToList());
        }

        public ActionResult AddToCart(int id)
        {
            var product = db.Products.Find(id);
            if (product != null && product.Quantità > 0)
            {
                product.Quantità--;

                var cartItem = new Cart
                {
                    ProductId = product.Id,
                    Quantità = 1,
                };

                db.Carts.Add(cartItem);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }

       

        public ActionResult Cart()
        {
            var cartItems = db.Carts.Include("Products").ToList();
            return View(cartItems);
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cartItem = db.Carts.Find(id);
            if (cartItem != null)
            {
                var product = db.Products.Find(cartItem.ProductId);
                if (product != null)
                {
                    product.Quantità++;
                    db.Carts.Remove(cartItem);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("Cart");
        }

        public ActionResult ProceedToSale()
        {
            var cartItems = db.Carts.Include("Products").ToList(); 
            var hasPrescription = cartItems.Any(c => c.Products.Ricetta);

            ViewBag.HasPrescription = hasPrescription;
            return View();
        }

        [HttpPost]
        public ActionResult CompleteOrder(string codiceFiscale, string ricetta)
        {
            var cartItems = db.Carts.Include("Products").ToList();
            var hasPrescription = cartItems.Any(c => c.Products.Ricetta);

            if (!string.IsNullOrEmpty(codiceFiscale))
            {
                foreach (var cartItem in cartItems)
                {
                    cartItem.CodiceFiscale = codiceFiscale;
                    cartItem.RicettaMedica = ricetta;
                }
                db.SaveChanges();

                var saleItems = new List<Sale>();
                foreach (var cartItem in cartItems)
                {
                    var saleItem = new Sale
                    {
                        CodiceFiscale = cartItem.CodiceFiscale,
                        DataVendita = DateTime.Now,
                        ProductId = cartItem.ProductId,
                        Quantità = cartItem.Quantità,
                        RicettaMedica = cartItem.RicettaMedica
                    };
                    saleItems.Add(saleItem);
                }
                db.Sales.AddRange(saleItems);
                db.SaveChanges();

                db.Carts.RemoveRange(cartItems);
                db.SaveChanges();
            }

            ViewBag.HasPrescription = hasPrescription;
            return RedirectToAction("Index"); 
        }

        public ActionResult AllSalesJson()
        {
            var sales = db.Sales.ToList();
            return Json(sales, JsonRequestBehavior.AllowGet);
        }

    }
}
