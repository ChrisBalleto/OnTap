using OnTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.ViewModels
{
    public class HoursOfOperationViewModel
    {
        public Bar Bar { get; set; }

        public HoursOfOperation HoursOfOperation { get; set; }

        public IEnumerable<Models.DayOfWeek> DayOfWeeks { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? SundayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? SundayClose { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? MondayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? MondayClose { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? TuesdayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? TuesdayClose { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? WednesdayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? WednesdayClose { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? ThursdayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? ThursdayClose { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? FridayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? FridayClose { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? SaturdayOpen { get; set; }
        [DisplayFormat(DataFormatString = "{0:hh:mm tt}")]
        public DateTime? SaturdayClose { get; set; }


    }
}