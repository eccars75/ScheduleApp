using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Display(Name = "Student's Name")]
        public string Student_Name { get; set; }

        public int Subject_Id { get; set; }
        [ForeignKey("Subject_Id")]
        public virtual Subjects Subjects { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Time")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime Start_Date { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "End Time")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy hh:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime End_Date { get; set; }
        [Display(Name = "Session Complete?")]
        public bool Completed { get; set; }
        [Display(Name = "Is No Show?")]
        public bool NoShow { get; set; }
        public int Rating { get; set; }

    }
}