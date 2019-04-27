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
            //checks if anyone is logged in
            try
            {
                var tutQry = (from tut in db.Tutors
                           where tut.Email == User.Identity.Name 
                           select tut).ToList();

                if (tutQry.Any())
                {
                    ViewBag.IsUser = false;
                }
                else
                {
                    ViewBag.IsUser = true;
                }
            }
            catch (Exception e)
            {
                ViewBag.IsUser = true;
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