using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class Special
    {
        public int Id { get; set; }

        public Bar Bar { get; set; }
        public int BarId { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public int DayOfWeekId { get; set; }

        public string SpecialDescription { get; set; }

        

        
    }
}