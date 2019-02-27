using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ScheduleApp.Models
{
    public class ScheduleAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public ScheduleAppContext() : base("name=ScheduleAppContext")
        {
        }

        public System.Data.Entity.DbSet<ScheduleApp.Models.Available> Availables { get; set; }

        public System.Data.Entity.DbSet<ScheduleApp.Models.Session> Sessions { get; set; }

        public System.Data.Entity.DbSet<ScheduleApp.Models.Subjects> Subjects { get; set; }

        public System.Data.Entity.DbSet<ScheduleApp.Models.Tutor> Tutors { get; set; }

        //public System.Data.Entity.DbSet<ScheduleApp.Models.TutorSubject> TutorSubjects { get; set; }
    }
}
