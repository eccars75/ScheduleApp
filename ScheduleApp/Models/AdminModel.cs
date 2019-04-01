using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class Admin
    {
        public int Id { get; set; }

        public string Email { get; set; }
        [Display(Name = "Admin's Name")]
        public string Admin_Name { get; set; }
    }
}