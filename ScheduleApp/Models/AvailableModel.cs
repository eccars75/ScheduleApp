using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class Available
    {
        //!!!!prob not going to be used
        public int Id{ get; set; }

        public string Tutor_Name { get; set; }
        //public string Tutor_Id { get; set; }
        //[ForeignKey("Tutor_Id")]
        //public virtual ApplicationUser Tutor { get; set; }

        public string Subject { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
    }
}