using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    public class ContainerController : Controller
    {
        private readonly VetContext _context;

        public ContainerController()
        {
            _context = new VetContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PosizioneFarmaci()
        {
            return View();
        }


        public JsonResult AllProductsJson()
        {
            var products = _context.Products.ToList();
            return Json(products);
        }
    }
}
