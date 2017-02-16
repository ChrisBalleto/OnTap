using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class Review
    {
        public int Id { get; set; }

        public Patron Patron { get; set; }
        public int PatronId { get; set; }

        public Bar Bar { get; set; }
        public int BarId { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public int? Rating { get; set; }
    }
}