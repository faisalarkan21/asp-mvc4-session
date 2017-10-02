using airlines_clone.helper;
using airlines_clone.Security;
using MvcApplication2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airlines_clone.Controllers
{
    [AuthorizationFilterUser]
    public class UserController : Controller
    {
        static tiket_reservationEntities db = new tiket_reservationEntities();


        // GET: User
        public ActionResult dashboard()
        {
            return View();
        }

        public ActionResult log_out()
        {
            Session.Remove("user");
            Session.Remove("email");
            Session.Remove("id");
            return RedirectToAction("index", "Home");
        }

		// Bug No 2 Fix
		public void RefreshAllTable()
		{
			foreach (var entity in db.ChangeTracker.Entries())
			{
				entity.Reload();
			}
		}

	

      
        





    }
}