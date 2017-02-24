using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class SearchBarsViewModel
    {
        public Patron Patron { get; set; }

        public BarGame BarGame {get;set;}

        public TapBeer TapBeer { get; set; }
        
        public SportsPackage SportsPackage { get; set; }

        public IEnumerable<Bar> Bars { get; set; }

        public IEnumerable<BarGame> BarGames { get; set; }

        public IEnumerable<SportsPackage> SportsPackages {get; set;}

        public IEnumerable<TapBeer> TapBeers { get; set; }

        public IEnumerable<State> States { get; set; }

        public IEnumerable<Special> Specials { get; set; }

        public IEnumerable<ZipCode> ZipCodes { get; set; }

        public IEnumerable<City> Cities { get; set; }

    }
}