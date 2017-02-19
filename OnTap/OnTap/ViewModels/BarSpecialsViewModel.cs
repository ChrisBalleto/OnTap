using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class BarSpecialsViewModel
    {
        public Bar Bar { get; set; }

        public IEnumerable<Models.DayOfWeek> DayOfWeeks{ get; set; }

        public Special Special { get; set; }

        public IEnumerable<Special> Specials { get; set; }
    }
}