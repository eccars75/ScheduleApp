using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleApp.Models
{
    public class TutorSchedule
    {
        public int Id { get; set; }

        public int Tutor_Id { get; set; }
        [ForeignKey("Tutor_Id")]
        public virtual Tutor Tutor { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, h:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd, h:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime EndTime { get; set; }

        //future implemnetation subjects
    }
}