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
            SendEmail(session);
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
                return RedirectToAction("SignUp");
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return View(session);
        }

        //for tutors to see upcoming sessions
        public ActionResult TutorsUpcoming()
        {
            var qry = (from ses in db.Sessions
                       where ses.Subjects.Tutor.Email.ToString() == User.Identity.Name && ses.Completed == false
                       select ses).ToList();
            ViewBag.Subject_Id = new SelectList(qry, "Id", "Subject");
            return View(qry);
        }

        //update session
        public ActionResult TutorsUpdateSession(int id, bool isNoShow)
        {
            ScheduleApp.Models.Session session = db.Sessions.Find(id);
            if (session != null)
            {
                session.NoShow = isNoShow;
                session.Completed = true;
                session.End_Date = DateTime.Now;
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("TutorsUpcoming");
            }
            ViewBag.Subject_Id = new SelectList(db.Subjects, "Id", "Subject", session.Subject_Id);
            return RedirectToAction("TutorsUpcoming");
        }

        public ActionResult Download ()
        { return View(); }

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
                var body = @"I'm sorry but your current appointment scheduled for " + session.Start_Date + " has been cancelled. Please reschedule at website here.<br>Thomas More Tutoring Center";

                var message = new MailMessage();
                message.To.Add(new MailAddress(session.Student_Name));
                message.From = new MailAddress("thomasmoretutoring@gmail.com");
                message.Subject = "Your appointment has been cancelled";
                message.Body = string.Format(body);
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "thomasmoretutoring@gmail.com",
                        Password = "!!changeThis"
                        //UserName = WebConfigurationManager.AppSettings["thomasmoretutoring@gmail.com"],
                        //Password = WebConfigurationManager.AppSettings["Nope nice try"]
                    };

                    //smtp.UseDefaultCredentials = false;
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
            }
            catch (Exception e)
            {
                //
            }
        }
    }
}
