using airlines_clone.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        static tiket_reservationEntities db = new tiket_reservationEntities();


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult login_user()
        {            
            if (Session["user"] != null)
            {
                return RedirectToAction("dashboard", "User");
            }

            return View();
        }


        public ActionResult Login_admin()
        {
            if (Session["admin"] != null)
            {
                return RedirectToAction("dashboard_admin", "Admin");
            }

            return View();
        }

        public void RefreshAllTable()
        {
            foreach (var entity in db.ChangeTracker.Entries())
            {
                entity.Reload();
            }
        }

        [HttpPost]
        public ActionResult Login_user(pembeli postPembeli)
        {

            RefreshAllTable();

            pembeli pb = db.pembeli.SingleOrDefault(u => u.email_pembeli == postPembeli.email_pembeli);

            if (pb == null)
            {
                ViewBag.htmlError = "has-error";
                ViewBag.errorMessage = "Username atau password anda salah.";
                return View();
            }

            bool comparePassword = PBKDF2Encription.VerifyHashedPassword(pb.password, postPembeli.password);


            if (postPembeli.email_pembeli == pb.email_pembeli && comparePassword)
            {
                Session["user"] = pb.nm_pembeli;
                Session["email"] = pb.email_pembeli;
                Session["id"] = pb.id_pembeli;
                return RedirectToAction("dashboard", "User");
            }
            else
            {
                ViewBag.htmlError = "has-error";
                ViewBag.errorMessage = "Username atau password anda salah";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login_admin(admin postAdmin)
        {
            RefreshAllTable();

            admin ad = db.admin.SingleOrDefault(u
                     => u.email_admin == postAdmin.email_admin);

            if (ad == null)
            {
                ViewBag.htmlError = "has-error";
                ViewBag.errorMessage =
                "Username atau password anda salah.";
                return View();
            }

            bool comparePassword =
            PBKDF2Encription.VerifyHashedPassword
            (ad.pass_admin, postAdmin.pass_admin);

            if (postAdmin.email_admin == ad.email_admin
                && comparePassword)
            {
                Session["admin"] = ad.nm_admin;
                Session["email"] = ad.email_admin;
                return RedirectToAction("dashboard_admin", "Admin");
            }
            else
            {
                ViewBag.htmlError = "has-error";
                ViewBag.errorMessage =
                "Username atau password anda salah";
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
