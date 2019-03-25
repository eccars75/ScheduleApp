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
    public class TutorSchedulesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TutorSchedules
        public ActionResult Index()
        {
            var tutorSchedules = db.TutorSchedules.Include(t => t.Tutor);
            return View(tutorSchedules.ToList());
        }

        // GET: TutorSchedules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            if (tutorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(tutorSchedule);
        }

        // GET: TutorSchedules/Create
        public ActionResult Create()
        {
            ViewBag.Tutor_Id = new SelectList(db.Tutors, "Id", "Email");
            return View();
        }

        // POST: TutorSchedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tutor_Id,StartTime,EndTime")] TutorSchedule tutorSchedule)
        {
            if (ModelState.IsValid)
            {
                db.TutorSchedules.Add(tutorSchedule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Tutor_Id = new SelectList(db.Tutors, "Id", "Email", tutorSchedule.Tutor_Id);
            return View(tutorSchedule);
        }

        // GET: TutorSchedules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            if (tutorSchedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tutor_Id = new SelectList(db.Tutors, "Id", "Email", tutorSchedule.Tutor_Id);
            return View(tutorSchedule);
        }

        // POST: TutorSchedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tutor_Id,StartTime,EndTime")] TutorSchedule tutorSchedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorSchedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Tutor_Id = new SelectList(db.Tutors, "Id", "Email", tutorSchedule.Tutor_Id);
            return View(tutorSchedule);
        }

        // GET: TutorSchedules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            if (tutorSchedule == null)
            {
                return HttpNotFound();
            }
            return View(tutorSchedule);
        }

        // POST: TutorSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutorSchedule tutorSchedule = db.TutorSchedules.Find(id);
            db.TutorSchedules.Remove(tutorSchedule);
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
