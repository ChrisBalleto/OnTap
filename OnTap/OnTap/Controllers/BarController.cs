﻿using OnTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
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
            var bar = _context.Bars.Include(b => b.TapBeers).SingleOrDefault(c => c.Id == id);
            var viewModel = new AddTapBeerViewModel
            {
                Bar = bar,
                TapBeers = bar.TapBeers,
            };
            
            return View(viewModel);
        }

        public ActionResult BarDashboard(int id)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);
            return View(bar);
        }

        [HttpPost]
        public ActionResult AddBeerToBar(int id, List<string> beers, List<string> beerNames, List<string> beerDescs, List<string> beerAbvs, List<string> beerImgs)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);

            for(int i = 0; i < beers.Count;i++)
            {
                var tapBeer = new TapBeer
                {

                    BreweryDatabaseId = beers[i],
                    Name = beerNames[i],
                    Description = beerDescs[i],
                    Abv = beerAbvs[i],
                    ImageLink = beerImgs[i],
                };
                tapBeer.Bars.Add(bar);
                bar.TapBeers.Add(tapBeer);
                _context.TapBeers.Add(tapBeer);
                _context.SaveChanges();
            }

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("AddBeerToBar", "Bar", new { Id = bar.Id });
            return Json(new { Url = redirectUrl });

            return View("AddBeerToBar","Bar", id);
        }

        public ActionResult AddSpecial(int id)
        {
            var bar = _context.Bars.Include(x => x.Specials).SingleOrDefault(c => c.Id == id);
            var dayofWeeks = _context.DayOfWeeks.ToList();
            var viewModel = new BarSpecialsViewModel
            {
                Specials = bar.GetSpecials,
                DayOfWeeks = dayofWeeks,
                Bar = bar,
            };

            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddSpecial(int id, BarSpecialsViewModel viewModel)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);
            
            
            var newSpecial = new Special
            {
                BarId = bar.Id,
                DayOfWeekId = viewModel.Special.DayOfWeekId,
                SpecialDescription = viewModel.Special.SpecialDescription
            };
            bar.Specials.Add(newSpecial);
            _context.Specials.Add(newSpecial);
            _context.SaveChanges();
            
            return RedirectToAction("AddSpecial", "Bar", bar.Id);
        }
        

        public ActionResult HoursOfOperation(int id)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);
            var daysOfWeek = _context.DayOfWeeks.ToList();
            var viewModel = new HoursOfOperationViewModel
            {
                Bar = bar,
                DayOfWeeks = daysOfWeek,
                SundayOpen = bar.HoursOfOperations.ElementAt(0).OpenTime.Value,
                SundayClose = bar.HoursOfOperations.ElementAt(0).OpenTime.Value,
                MondayOpen = bar.HoursOfOperations.ElementAt(1).OpenTime.Value,
                MondayClose = bar.HoursOfOperations.ElementAt(1).OpenTime.Value,
                TuesdayOpen = bar.HoursOfOperations.ElementAt(2).OpenTime.Value,
                TuesdayClose = bar.HoursOfOperations.ElementAt(2).OpenTime.Value,
                WednesdayOpen = bar.HoursOfOperations.ElementAt(3).OpenTime.Value,
                WednesdayClose = bar.HoursOfOperations.ElementAt(3).OpenTime.Value,
                ThursdayOpen = bar.HoursOfOperations.ElementAt(4).OpenTime.Value,
                ThursdayClose = bar.HoursOfOperations.ElementAt(4).OpenTime.Value,
                FridayOpen = bar.HoursOfOperations.ElementAt(5).OpenTime.Value,
                FridayClose = bar.HoursOfOperations.ElementAt(5).OpenTime.Value,
                SaturdayOpen = bar.HoursOfOperations.ElementAt(6).OpenTime.Value,
                SaturdayClose = bar.HoursOfOperations.ElementAt(6).OpenTime.Value,
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult HoursOfOperation(HoursOfOperationViewModel viewModel)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == viewModel.Bar.Id);

            foreach(var hours in _context.HoursOfOperations)
            {
                if(bar.Id == hours.BarId)
                {
                    _context.HoursOfOperations.Remove(hours);
                }
            }

            bar.HoursOfOperations.Clear();

            var hoursOfOpperationSunday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 1,
                OpenTime = viewModel.SundayOpen,
                CloseTime = viewModel.SundayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationSunday);
            bar.HoursOfOperations.Add(hoursOfOpperationSunday);
            var hoursOfOpperationMonday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 2,
                OpenTime = viewModel.MondayOpen,
                CloseTime = viewModel.MondayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationMonday);
            bar.HoursOfOperations.Add(hoursOfOpperationMonday);
            var hoursOfOpperationTuesday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 3,
                OpenTime = viewModel.TuesdayOpen,
                CloseTime = viewModel.TuesdayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationTuesday);
            bar.HoursOfOperations.Add(hoursOfOpperationTuesday);
            var hoursOfOpperationWednesday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 4,
                OpenTime = viewModel.WednesdayOpen,
                CloseTime = viewModel.WednesdayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationWednesday);
            bar.HoursOfOperations.Add(hoursOfOpperationWednesday);
            var hoursOfOpperationThursday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 5,
                OpenTime = viewModel.ThursdayOpen,
                CloseTime = viewModel.ThursdayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationThursday);
            bar.HoursOfOperations.Add(hoursOfOpperationThursday);
            var hoursOfOpperationFriday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 6,
                OpenTime = viewModel.FridayOpen,
                CloseTime = viewModel.FridayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationFriday);
            bar.HoursOfOperations.Add(hoursOfOpperationFriday);
            var hoursOfOpperationSaturday = new HoursOfOperation
            {
                BarId = bar.Id,
                DayOfWeekId = 7,
                OpenTime = viewModel.SaturdayOpen,
                CloseTime = viewModel.SaturdayClose
            };
            _context.HoursOfOperations.Add(hoursOfOpperationSaturday);
            bar.HoursOfOperations.Add(hoursOfOpperationSaturday);

            _context.SaveChanges();

            return View("BarDashboard", bar);
        }



    }
}