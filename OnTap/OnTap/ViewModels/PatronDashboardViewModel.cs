using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class PatronDashboardViewModel
    {
        public Patron Patron { get; set; }
        public IEnumerable<Models.DayOfWeek> DaysofWeek { get; set; }
        public System.DateTime SysDayOfWeek { get; set; }
        public Models.DayOfWeek ModDayOfWeek { get; set; }
        public string StringDayOfWeek { get; set; }
    }
}