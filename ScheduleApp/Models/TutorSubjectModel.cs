using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class TutorSubject
    {
        public int Id { get; set; }

        public string Tutor_Name { get; set; }
        //string Tutor_Id { get; set; }
        //[ForeignKey("Tutor_Id")]
        //public virtual ApplicationUser Tutor { get; set; }

        public List<string> Subjects{ get; set; }
    }
}