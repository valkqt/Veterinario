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
            var containers = _context.Containers.ToList();
            return View(containers);
        }
    }
}
