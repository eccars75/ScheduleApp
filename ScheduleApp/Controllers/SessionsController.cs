using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using ScheduleApp.Models;

namespace ScheduleApp.Controllers
{
    public class SessionsController : Controller
    {
        private ScheduleAppContext db = new ScheduleAppContext();
        private char whoDelete;

        // GET: Sessions
        [Authorize]
        public ActionResult Index()
        {
            var qry = (from ses in db.Admins
                       where ses.Email == User.Identity.Name
                       select ses).ToList();

            if (qry.Any())
            {
                var sessions = db.Sessions.Include(s => s.Subjects);
                return View(sessions.ToList());
            }
            return RedirectToAction("Index", "Home");
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
        [Authorize]
        public ActionResult Create()
        {
            var qry = (from ses in db.Admins
                       where ses.Email == User.Identity.Name
                       select ses).ToList();

            if (qry.Any())
            {
                ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Delete(int? id, char whoDel)
        {
            ViewBag.WhoDelete = whoDel;
            whoDelete = whoDel;
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
        public ActionResult DeleteConfirmed(int id, char whoDel)
        {
            whoDelete = whoDel;
            Session session = db.Sessions.Find(id);
            if (whoDelete != 'u')
            {
                SendEmail(session);
            }
            whoDelete = 'a';
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

        [Authorize]
        public ActionResult SignUp()
        {
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "TutorName");
            ViewBag.TutorList = db.TutorSchedules.ToList();
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Id,Student_Name,Subject_Id,Start_Date,End_Date,Completed,NoShow,Rating")] Session session)
        {
            bool withinSchedule = false;

            var tempSub = db.Subjects.Find(session.Subject_Id);
            //var temp = (session.Start_Date == tempSub.

            var conflicts = (from ses in db.Sessions
                             from sub in db.Subjects
                             where sub.Id == session.Subject_Id && ses.Subjects.Tutor_Id == sub.Tutor_Id &&
                                ses.Start_Date >= session.Start_Date && ses.Start_Date < session.End_Date &&
                                ses.End_Date > session.Start_Date && ses.End_Date <= session.End_Date
                             select ses).ToList();

            var TutorSchedul = (from time in db.TutorSchedules
                                from sub in db.Subjects
                                where sub.Id == session.Subject_Id && sub.Tutor.Id == time.Tutor.Id
                                select time).ToList();

            //check 1 time within schedule
            foreach (var item in TutorSchedul)
            {
                if (session.Start_Date.DayOfWeek == item.StartTime.DayOfWeek &&
                    session.Start_Date.TimeOfDay >= item.StartTime.TimeOfDay && session.Start_Date.TimeOfDay < item.EndTime.TimeOfDay &&
                    session.End_Date.TimeOfDay > item.StartTime.TimeOfDay && session.End_Date.TimeOfDay <= item.EndTime.TimeOfDay)
                {
                    withinSchedule = true;
                }
            }

            if (!withinSchedule)
            {
                ModelState.AddModelError("Start_Date", "The tutor is not scheduled for this time");
                ModelState.AddModelError("End_Date", "The tutor is not scheduled for this time");
            }

            //check 2 time in the future
            if (session.Start_Date < DateTime.Now && ModelState.IsValid)
            {
                ModelState.AddModelError("Start_Date", "Enter a Date in the future");
            }

            //check 3 end date is greater than start date
            if (session.End_Date < session.Start_Date && ModelState.IsValid)
            {
                ModelState.AddModelError("End_Date", "Enter a Date in the future");
            }

            //check 4 any conflicts
            if (conflicts.Any() && ModelState.IsValid)
            {
                ModelState.AddModelError("Start_Date", "Time is Unvailable");
            }

            if (ModelState.IsValid)
            {
                session.Student_Name = System.Web.HttpContext.Current.User.Identity.Name;
                db.Sessions.Add(session);
                db.SaveChanges();
                ViewBag.Message = "Sign Up Successful";
            }

            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "TutorName");
            ViewBag.TutorList = db.TutorSchedules.ToList();
            return View(session);
        }

        //for users to see upcoming sessions
        [Authorize]
        public ActionResult UsersUpcoming()
        {
            var qry = (from ses in db.Sessions
                       where ses.Student_Name == User.Identity.Name && ses.Completed == false && ses.Start_Date > DateTime.Now
                       select ses).ToList();
            ViewBag.Subject_Id = new SelectList(qry, "Id", "Subject");
            return View(qry);
        }

        //for tutors to see upcoming sessions
        [Authorize]
        public ActionResult TutorsUpcoming()
        {
            var qry = (from ses in db.Sessions
                       where ses.Subjects.Tutor.Email == User.Identity.Name && ses.Completed == false
                       select ses).ToList();
            ViewBag.Subject_Id = new SelectList(qry, "Id", "Subject");
            return View(qry);
        }

        //update session
        public ActionResult TutorsUpdateSession(int id, bool isNoShow)
        {
            //more logic should have been placed here :(
            ScheduleApp.Models.Session session = db.Sessions.Find(id);
            if (session != null)
            {
                session.NoShow = isNoShow;
                session.Completed = true;
                // if noshow then dont change endtime or change end time if no shows during appointment
                session.End_Date = DateTime.Now;
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return RedirectToAction("TutorsUpcoming");
        }

        //download csv for sessions model
        public FileContentResult DownloadSessionsCSV()
        {
            var qry = (from data in db.Sessions
                       select data).AsEnumerable();
            string csv = string.Concat(
             qry.Select(
                    session => string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}\n", session.Id, session.Student_Name, session.Subjects.Subject, session.Subjects.Tutor.Tutor_Name, session.Start_Date, session.End_Date, session.Completed, session.NoShow, session.Rating)));
            return File(new System.Text.UTF8Encoding().GetBytes(csv), "text/csv", "SessionsReport.csv");
        }

        //sends email, thomas more email service seems to send on a delay, tested with non thomas more email account
        public void SendEmail(Session session)
        {
            try
            {
                //string emailTo = session.Student_Name;
                var body = @"DO NOT REPLY<br>I'm sorry but your current appointment scheduled for " + session.Start_Date + " has been cancelled. Please reschedule at website here.<br>Thomas More Tutoring Center";

                var message = new MailMessage();
                message.To.Add(new MailAddress(session.Student_Name));
                message.From = new MailAddress("thomasmoretutoring@gmail.com");
                message.Subject = "Your appointment has been cancelled -DO NOT REPLY-";
                message.Body = string.Format(body);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "thomasmoretutoring@gmail.com",
                        Password = "!!changeThis!!"
                    };

                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
            }
            catch (Exception e) { }
        }
    }
}
