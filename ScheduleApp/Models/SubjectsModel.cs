using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class Subjects
    {
        public int Id { get; set; }

        
        public int Tutor_Id { get; set; }
        [ForeignKey("Tutor_Id")]
        public virtual Tutor Tutor { get; set; }

        public string Subject { get; set; }
    }
}