using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Veterinario3.Models;

namespace Veterinario3.Controllers
{
    public class HomeController : Controller
    {
        private VetContext db = new VetContext();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("veterinario"))
                {
                    return RedirectToAction("Animals", "Vet");
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            var loggedUser = db.Users.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
            if ( loggedUser == null)
            {
                TempData["ErrorLogin"] = "Credenziali non valide.";
                return RedirectToAction("Login");
            }
            FormsAuthentication.SetAuthCookie(loggedUser.id.ToString(), true);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}