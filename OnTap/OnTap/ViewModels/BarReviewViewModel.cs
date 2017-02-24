using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class BarReviewViewModel
    {
        public Patron Patron { get; set; }
        public Bar Bar { get; set; }
        public Review Review { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}