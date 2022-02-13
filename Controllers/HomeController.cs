using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Pasticceria.Controllers
{
    public class HomeController : Controller
    {
        private Model2 db = new Model2();

        public ActionResult Index()
        {

            return View(db.Product.ToList());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Logins u)
        {
            Logins ToLog = db.Logins.Where(user => user.username == u.username).Where(pass => pass.password == u.password).FirstOrDefault();
            if (ToLog != null)
            {
                FormsAuthentication.SetAuthCookie(ToLog.username, true);
                return RedirectToAction("List", "Products");
            }
            else
            {
                ViewBag.Errore = "Username e/o Password Incorretti";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
    }
}