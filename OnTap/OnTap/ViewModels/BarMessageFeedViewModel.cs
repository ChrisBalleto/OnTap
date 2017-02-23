using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;
using System.ComponentModel.DataAnnotations;

namespace OnTap.ViewModels
{
    public class BarMessageFeedViewModel
    {
        public Bar Bar { get; set; }
        public FeedMessage FeedMessage { get; set; }
        public IEnumerable<FeedMessage> FeedMessages { get; set; }
    }
}