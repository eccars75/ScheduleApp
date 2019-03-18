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
    public class SessionsController : Controller
    {
        private ScheduleAppContext db = new ScheduleAppContext();

        // GET: Sessions
        public ActionResult Index()
        {
            var sessions = db.Sessions.Include(s => s.Subjects);
            return View(sessions.ToList());
        }

        // GET: Sessions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // GET: Sessions/Create
        //[Authorize]
        public ActionResult Create()
        {
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Create([Bind(Include = "Id,Student_Name,Subject_Id,Start_Date,End_Date,Completed,NoShow,Rating")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return View(session);
        }

        // GET: Sessions/Edit/5
        //[Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult Edit([Bind(Include = "Id,Student_Name,Subject_Id,Start_Date,End_Date,Completed,NoShow,Rating")] Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return View(session);
        }

        // GET: Sessions/Delete/5
        //[Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
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

        //[Authorize]
        public ActionResult SignUp()
        {
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "TutorName");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize]
        public ActionResult SignUp([Bind(Include = "Id,Student_Name,Subject_Id,Start_Date,End_Date,Completed,NoShow,Rating")] Session session)
        {
            if (ModelState.IsValid)
            {

                session.Student_Name = System.Web.HttpContext.Current.User.Identity.Name;
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return View(session);
        }

        //for tutors to see upcoming sessions
        public ActionResult TutorsUpcoming()
        {
            var qry = (from ses in db.Sessions
                       where ses.Subjects.Tutor.ToString() == User.Identity.Name && ses.Completed == false
                       select ses).ToList();
            ViewBag.Subject_Id = new SelectList(qry, "Id", "Subject");
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TutorsUpcoming([Bind(Include = "Id,Student_Name,Subject_Id,Start_Date,End_Date,Completed,NoShow,Rating")] Session session)
        {
            if (ModelState.IsValid)
            {

                session.Student_Name = System.Web.HttpContext.Current.User.Identity.Name;
                db.Sessions.Add(session);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return View(session);
        }

        public FileContentResult DownloadCSV()
        {
            //This GetAllData method will fetch data from server and create a comma seperate string.
            //var data = GetAllData();
            var bytes = UnicodeEncoding.Unicode.GetBytes(data);
            return File(bytes, "text/csv");
        }
    }
}
