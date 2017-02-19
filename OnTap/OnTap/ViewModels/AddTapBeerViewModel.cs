using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class AddTapBeerViewModel
    {
        public Bar Bar { get; set; }

        public IEnumerable<TapBeer> TapBeers { get; set; }

        public TapBeer TapBeer { get; set; }

    }
}