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
    public class TutorSubjectsController : Controller
    {
        private ScheduleAppContext db = new ScheduleAppContext();

        // GET: TutorSubjects
        public ActionResult Index()
        {
            return View(db.TutorSubjects.ToList());
        }

        // GET: TutorSubjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSubject tutorSubject = db.TutorSubjects.Find(id);
            if (tutorSubject == null)
            {
                return HttpNotFound();
            }
            return View(tutorSubject);
        }

        // GET: TutorSubjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TutorSubjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Tutor_Name")] TutorSubject tutorSubject)
        {
            if (ModelState.IsValid)
            {
                db.TutorSubjects.Add(tutorSubject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tutorSubject);
        }

        // GET: TutorSubjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSubject tutorSubject = db.TutorSubjects.Find(id);
            if (tutorSubject == null)
            {
                return HttpNotFound();
            }
            return View(tutorSubject);
        }

        // POST: TutorSubjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Tutor_Name")] TutorSubject tutorSubject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tutorSubject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tutorSubject);
        }

        // GET: TutorSubjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TutorSubject tutorSubject = db.TutorSubjects.Find(id);
            if (tutorSubject == null)
            {
                return HttpNotFound();
            }
            return View(tutorSubject);
        }

        // POST: TutorSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TutorSubject tutorSubject = db.TutorSubjects.Find(id);
            db.TutorSubjects.Remove(tutorSubject);
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
