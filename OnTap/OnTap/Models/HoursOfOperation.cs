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

        public DateTime DayOfWeek { get; set; }
        public int DayOfWeekId { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }


    }
}