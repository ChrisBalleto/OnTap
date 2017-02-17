using OnTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenOfBeer.BreweryDb;
using ZenOfBeer.BreweryDb.Pcl.Public;
using OnTap.ViewModels;

namespace OnTap.Controllers
{
    public class BarController : Controller
    {
        private ApplicationDbContext _context;
        public BarController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Bar
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddBeerToBar(int id)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);
            return View(bar);
        }

        public ActionResult BarDashboard(int id)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);
            return View(bar);
        }

        [HttpPost]
        public ActionResult AddBeerToBar(int id, List<string> beers)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);

            for(var i = 0; i < beers.Count; i++)
            {
                var tapBeer = new TapBeer
                {
                    BreweryDatabaseId = beers[i],                 
                };
                tapBeer.Bars.Add(bar);
                bar.TapBeers.Add(tapBeer);
            }
            

            return RedirectToAction("BarDashboard", id);
        }

        public ActionResult HoursOfOperation(int id)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);
            var daysOfWeek = _context.DayOfWeeks.ToList();
            var viewModel = new HoursOfOperationViewModel
            {
                Bar = bar,
                DayOfWeeks = daysOfWeek,
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult HoursOfOperation(int id, List<int> dayIds, List<DateTime> openTimes, List<DateTime> closeTimes)
        {
            return View();
        }



    }
}