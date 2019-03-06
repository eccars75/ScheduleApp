using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class MyDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.Now;
        }
    }

        public class Session
    {
        public int Id { get; set; }

        [Display(Name = "Student's Name")]
        public string Student_Name { get; set; }

        //[Display(Name = "Tutor's Name")]
        //public string Tutor_Name { get; set; }
        //public string Subject { get; set; }

        public int Subject_Id { get; set; }
        [ForeignKey("Subject_Id")]
        public virtual Subjects Subjects { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Time")]
        [MyDate(ErrorMessage ="Please pick a date in the future")]
        public DateTime Start_Date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Time")]
        public DateTime End_Date { get; set; }
        [Display(Name = "Session Complete?")]
        public bool Completed { get; set; }
        [Display(Name = "Is No Show?")]
        public bool NoShow { get; set; }
        public int Rating { get; set; }


        //public string Student_Id { get; set; }
        //[ForeignKey("Student_Id")]
        //public virtual ApplicationUser Student { get; set; }

        //public string Tutor_Id { get; set; }
        //[ForeignKey("Tutor_Id")]
        //public virtual ApplicationUser Tutor { get; set; }
    }
}