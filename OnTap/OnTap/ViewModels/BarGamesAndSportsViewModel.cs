using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnTap.Models;

namespace OnTap.ViewModels
{
    public class BarGamesAndSportsViewModel
    {
        public Bar Bar { get; set; }
        public SportsPackage SportsPackage { get; set; }
        public BarGame BarGame { get; set; }
        public IEnumerable<BarGame> BarGames { get; set; }
        public IEnumerable<SportsPackage> SportsPackages { get; set; }

    }
}