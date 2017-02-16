using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnTap.Models
{
    public class TapBeer
    {
        public TapBeer()
        {
            this.Bars = new HashSet<Bar>();
        }
        public int Id { get; set; }

        public string BreweryDatabaseId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Abv { get; set; }

        public string ImageLink { get; set; }

        public virtual ICollection<Bar> Bars { get; set; }


    }
}