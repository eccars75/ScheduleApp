using ScheduleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScheduleApp.Controllers
{
    public class HomeController : Controller
    {
        private ScheduleAppContext db = new ScheduleAppContext();

        public ActionResult Index()
        {
            try
            {
                var qry = (from ses in db.Tutors
                           where ses.Email == User.Identity.Name
                           select ses).ToList();

                if (qry.Any())
                {
                    ViewBag.IsTutor = true;
                }
                else
                {
                    ViewBag.IsTutor = false;
                }
            }
            catch (Exception e)
            {
                ViewBag.IsTutor = false;
            }

            return View();
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

        public ActionResult Admin()
        {
            var qry = (from ses in db.Admins
                       where ses.Email == User.Identity.Name
                       select ses).ToList();

            if (qry.Any())
                return View();

            return RedirectToAction("Index", "Home");
        }
    }
}