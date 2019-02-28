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
        public int Id { get; set; }

        public string Email { get; set; }
        [Display(Name = "Tutor's Name")]
        public string Tutor_Name { get; set; }

    }
}