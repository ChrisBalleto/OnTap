using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class HoursOfOperation
    {
        public int Id { get; set; }

        public Bar Bar { get; set; }
        public int BarId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public int DayOfWeekId { get; set; }

        [Display(Name = "Open Time hh:mm AM/PM")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? OpenTime { get; set; }

        [Display(Name = "Close Time hh:mm AM/PM")]
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? CloseTime { get; set; }


    }
}