using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
    public class AvailablesController : Controller
    {
        private ScheduleAppContext db = new ScheduleAppContext();

        string name = "Bob";
        int startTime = 8;
        int endTime = 8;

        // GET: Availables
        public ActionResult Index()
        {
            return View(db.Availables.ToList());
        }

        // GET: Availables/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // GET: Availables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Availables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tutor_Name,Subject,Start_Date,End_Date")] Available available)
        {
            if (ModelState.IsValid)
            {
                db.Availables.Add(available);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(available);
        }

        // Creates available times for a week out
        // GET: Availables/Create
        public ActionResult CreateWeek()
        {
            return View();
        }

        // POST: Availables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateWeek([Bind(Include = "Id,Tutor_Name,Subject,Start_Date,End_Date")] Available available)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Availables.Add(available);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //gets monday of next week
            var currDate = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + (int)DayOfWeek.Monday);
            while (true)
            {
                if (currDate.Day.Equals(DayOfWeek.Friday) && currDate.TimeOfDay.Equals(DateTime.Parse))
            }


            return View(available);
        }

        // GET: Availables/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // POST: Availables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tutor_Name,Subject,Start_Date,End_Date")] Available available)
        {
            if (ModelState.IsValid)
            {
                db.Entry(available).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(available);
        }

        // GET: Availables/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Available available = db.Availables.Find(id);
            if (available == null)
            {
                return HttpNotFound();
            }
            return View(available);
        }

        // POST: Availables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Available available = db.Availables.Find(id);
            db.Availables.Remove(available);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
