using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class DayOfWeek
    {
        public int Id { get; set; }

        public string DayOfWeekName { get; set; }
    }
}