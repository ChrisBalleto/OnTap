using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class BarDashboardViewModel
    {
        public Bar Bar { get; set; }
        public List<Models.DayOfWeek> DayOfWeeks { get; set; }
        public List<HoursOfOperation> HoursOfOperations { get; set; }
        public List<TapBeer> TapBeers { get; set; }
        public List<Special> Specials { get; set; }
        public List<SportsPackage> SportsPackages { get; set; }
        public List<FeedMessage> FeedMessages { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Patron> Followers { get; set; }
        public List<Review> BarReviews { get; set; }
    }
}