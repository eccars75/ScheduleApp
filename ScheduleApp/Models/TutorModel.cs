using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class Tutor
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }
        public string Tutor_Name { get; set; }
        //string Tutor_Id { get; set; }
        //[ForeignKey("Tutor_Id")]
        //public virtual ApplicationUser Tutor { get; set; }
        //todo: create separate model for just subjects
        //public string Subjects{ get; set; }
    }
}