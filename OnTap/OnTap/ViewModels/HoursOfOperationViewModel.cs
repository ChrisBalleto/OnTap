using OnTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnTap.ViewModels
{
    public class HoursOfOperationViewModel
    {
        public Bar Bar { get; set; }
        public HoursOfOperation HoursOfOperation { get; set; }

        public IEnumerable<Models.DayOfWeek> DayOfWeeks { get; set; }           
        public string MondayOpen { get; set; }
        public string MondayClose { get; set; }


    }
}