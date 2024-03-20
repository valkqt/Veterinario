using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Veterinario3.Models;
using System.Data.Entity;

namespace Veterinario3.Controllers
{
    public class BoxController : Controller
    {
        private readonly VetContext _context;

        public BoxController()
        {
            _context = new VetContext();
        }

        // Azione per la semina dei dati
        public ActionResult SeedData()
        {
            // Creazione di due armadietti
            var container1 = new Container { CodiceArmadietto = "A1" };
            var container2 = new Container { CodiceArmadietto = "A2" };

            _context.Containers.Add(container1);
            _context.Containers.Add(container2);
            _context.SaveChanges();

            for (int i = 1; i <= 5; i++)
            {
                var box1 = new Box
                {
                    CodiceCassetto = "C" + i,
                    CodiceProdotto = "P" + i,
                    ContainerId = container1.id 
                };
                _context.Boxes.Add(box1);

                var box2 = new Box
                {
                    CodiceCassetto = "C" + (i + 5), 
                    CodiceProdotto = "P" + (i + 5),
                    ContainerId = container2.id 
                };
                _context.Boxes.Add(box2);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            var boxes = _context.Boxes.Include("Container").ToList();
            return View(boxes);
        }
    }
}
