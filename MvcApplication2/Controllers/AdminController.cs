using airlines_clone.helper;
using airlines_clone.Models;
using airlines_clone.Security.tiket_airlines.Security;
using MvcApplication2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace airlines_clone.Controllers
{
     [AuthorizationFilterAdmin]
    public class AdminController : Controller
    {

        static tiket_reservationEntities db = new tiket_reservationEntities();

		// Bug 3 Fix
		public void RefreshAllTable()
		{
			foreach (var entity in db.ChangeTracker.Entries())
			{
				entity.Reload();
			}
		}

		// GET: Admin
		public ActionResult dashboard_admin()
        {

            Statistik statistik = new Statistik();

            statistik.total_user = db.detil_pesan_tiket.Count();
            statistik.user_lunas = db.detil_pesan_tiket.Where(u
            => u.total_transfer != 0).Count();

            statistik.user_belum_lunas = db.detil_pesan_tiket.Where(u
            => u.total_transfer == 0).Count();


            var checkPembeli = db.detil_pesan_tiket;

            // cek pembeli ada atau engga 
            if (checkPembeli.Count() == 0 )
            {
                // biarkan kosong.

            }
            else
            {
				// Bug # 1 Fix 
				statistik.uang_estimasi = ConvertCurrency.
                ToRupiah(db.detil_pesan_tiket.Select(u
                => u.harga_tiket).Sum());

                statistik.uang_diterima = ConvertCurrency.
                ToRupiah(db.detil_pesan_tiket.Select(u
                => u.total_transfer).Sum());

                decimal estimasi = db.detil_pesan_tiket.Select(u
                => u.harga_tiket).Sum();
                decimal uangDiterima = db.detil_pesan_tiket.Select(u
                => u.total_transfer).Sum();
                statistik.selisiPendapatan = ConvertCurrency.
                ToRupiah(estimasi - uangDiterima);


            }

            statistik.user_validasi = db.pembeli_validasi.Where(u
            => u.uang_transfer_validasi != null).Count();

            return View(statistik);
        }

        public ActionResult log_out()
        {
            Session.Remove("admin");
            Session.Remove("email");
            return RedirectToAction("index", "Home");
        }

       




    }
}