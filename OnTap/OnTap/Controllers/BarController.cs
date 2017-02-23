using OnTap.Models;
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
        public ActionResult EditBar()
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.SingleOrDefault(c => c.Email == currentUser);

            if (bar == null)
                return HttpNotFound();

            var roleNames = _context.RoleNames.ToList();
            var cities = _context.Cities.ToList();
            var states = _context.States.ToList();
            var zipCodes = _context.ZipCodes.ToList();
            var viewModel = new RegisterBarViewModel
            {
                Bar = bar,
                Cities = cities,
                RoleNames = roleNames,
                States = states,
                ZipCodes = zipCodes,
            };
            return View(viewModel);
        }

        public ActionResult SearchBars()
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons
                .Include(p => p.FollowedBars)
                .Include(z => z.City)
                .Include(y => y.State)
                .Include(z => z.ZipCode)
                .SingleOrDefault(c => c.Email == currentUser);
            var barGames = _context.BarGames.ToList();
            var sportsPackages = _context.SportsPackages.ToList();
            var specials = _context.Specials.ToList();
            var tapBeers = _context.TapBeers.ToList();
            var states = _context.States.ToList();
            var zipCodes = _context.ZipCodes.ToList();
            var cities = _context.Cities.ToList();
            var daysOfWeek = _context.DayOfWeeks.ToList();
            var bars = _context.Bars
                .Include(x => x.ZipCode)
                .Include(x => x.City)
                .Include(x => x.State)
                .Include(x => x.FeedMessages)
                .Include(x => x.BarGames)
                .Include(x => x.Specials)
                .Include(x => x.SportsPackages)
                .Include(x => x.TapBeers)
                .Include(x => x.Followers)
                .Include(x => x.HoursOfOperations)
                .Include(x => x.BarReviews).ToList();
            var viewModel = new SearchBarsViewModel
            {
                Bars = bars,
                BarGames = barGames,
                SportsPackages = sportsPackages,
                TapBeers = tapBeers,
                States = states,
                Cities = cities,
                ZipCodes = zipCodes,
                Specials = specials,
            };
            return View(viewModel);
        }
        public ActionResult BarDashboard()
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars
            .Include(z => z.City)
            .Include(y => y.State)
            .Include(z => z.ZipCode)
            .Include(x => x.FeedMessages)
            .Include(x => x.BarGames)
            .Include(x => x.SportsPackages)
            .Include(x => x.TapBeers)
            .Include(x => x.Followers)
            .Include(x => x.HoursOfOperations)
            .Include(x => x.BarReviews)
            .SingleOrDefault(c => c.Email == currentUser);

            var viewModel = new BarDashboardViewModel()
            {
                Bar = bar,
                DayOfWeeks = _context.DayOfWeeks.ToList()
        };

            return View(viewModel);
        }
        

        public ActionResult BarMessageFeed()
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(x => x.FeedMessages).SingleOrDefault(c => c.Email == currentUser);

            var viewModel = new BarMessageFeedViewModel
            {
                Bar = bar,
                FeedMessages = bar.FeedMessages,
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BarMessageFeed( BarMessageFeedViewModel viewModel)
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(x => x.FeedMessages).SingleOrDefault(c => c.Email == currentUser);

            var message = new FeedMessage()
            {
                FromName = bar.BarName,
                Content = viewModel.FeedMessage.Content,
                Created = DateTime.Now,
                Subject = viewModel.FeedMessage.Subject,
            };
            bar.FeedMessages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("BarDashboard", "Bar");
        }
        [HttpPost]
        public ActionResult RemoveMessage(int id, int id2)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id2);
            var message = _context.FeedMessages.SingleOrDefault(c => c.Id == id);
            bar.FeedMessages.Remove(message);
            _context.SaveChanges();

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("BarDashboard", "Bar", new { Id = id2 });
            return Json(new { Url = redirectUrl });
        }


        public ActionResult AddBarGamesAndSports()
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(x => x.BarGames).Include(c => c.SportsPackages).SingleOrDefault(c => c.Email == currentUser);
            var sportsPackages = _context.SportsPackages.ToList();
            var barGames = _context.BarGames.ToList();
            var viewModel = new BarGamesAndSportsViewModel
            {
                BarGames = barGames,
                SportsPackages = sportsPackages,
                Bar = bar,
            };
            return View(viewModel);
        }

        public ActionResult RemoveBarGame(int id, int id2)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id2);
            var barGame = _context.BarGames.SingleOrDefault(c => c.Id == id);
            bar.BarGames.Remove(barGame);
            barGame.Bars.Remove(bar);
            _context.SaveChanges();

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("AddBarGamesAndSports", "Bar", new { Id = id2 });
            return Json(new { Url = redirectUrl });
        }
        public ActionResult RemoveSportsPackage(int id, int id2)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id2);
            var sportsPackage = _context.SportsPackages.SingleOrDefault(c => c.Id == id);
            bar.SportsPackages.Remove(sportsPackage);
            sportsPackage.Bars.Remove(bar);
            _context.SaveChanges();

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("AddBarGamesAndSports", "Bar", new { Id = id2 });
            return Json(new { Url = redirectUrl });

        }

        [HttpPost]
        public ActionResult AddBarGamesAndSports( BarGamesAndSportsViewModel viewModel)
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(x => x.BarGames).Include(c => c.SportsPackages).SingleOrDefault(c => c.Email == currentUser);
            if (viewModel.BarGame.Id > 0)
            {
                var barGame = _context.BarGames.SingleOrDefault(c => c.Id == viewModel.BarGame.Id);
                bar.BarGames.Add(barGame);
                barGame.Bars.Add(bar);
                _context.SaveChanges();
            }
            if (viewModel.SportsPackage.Id > 0)
            {
                var sportsPackage = _context.SportsPackages.SingleOrDefault(c => c.Id == viewModel.SportsPackage.Id);
                bar.SportsPackages.Add(sportsPackage);
                sportsPackage.Bars.Add(bar);
                _context.SaveChanges();
            }



            return RedirectToAction("AddBarGamesAndSports");
        }

        [HttpPost]
        public ActionResult AddSportsPackage(int id, BarGamesAndSportsViewModel viewModel)
        {
            var bar = _context.Bars.SingleOrDefault(c => c.Id == viewModel.Bar.Id);
            var sportsPackage = _context.SportsPackages.SingleOrDefault(c => c.Id == viewModel.SportsPackage.Id);
            bar.SportsPackages.Add(sportsPackage);
            sportsPackage.Bars.Add(bar);
            _context.SaveChanges();

            return RedirectToAction("AddBarGamesAndSports");
        }

        public ActionResult AddBeerToBar()
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(b => b.TapBeers).SingleOrDefault(c => c.Email == currentUser);
            var viewModel = new AddTapBeerViewModel
            {
                Bar = bar,
                TapBeers = bar.TapBeers,
            };
            
            return View(viewModel);
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
        }

        public ActionResult AddSpecial()
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(x => x.Specials).SingleOrDefault(c => c.Email == currentUser);
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
        public ActionResult AddSpecial( BarSpecialsViewModel viewModel)
        {
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.Include(x => x.Specials).SingleOrDefault(c => c.Email == currentUser);


            var newSpecial = new Special
            {
                BarId = bar.Id,
                DayOfWeekId = viewModel.Special.DayOfWeekId,
                SpecialDescription = viewModel.Special.SpecialDescription
            };
            bar.Specials.Add(newSpecial);
            _context.Specials.Add(newSpecial);
            _context.SaveChanges();
            
            return RedirectToAction("AddSpecial", "Bar");
        }
        

        public ActionResult HoursOfOperation()
        {
            var daysInWeek = 7;
            var currentUser = User.Identity.Name;
            var bar = _context.Bars.SingleOrDefault(c => c.Email == currentUser);
            var daysOfWeek = _context.DayOfWeeks.ToList();
            if (bar.HoursOfOperations.Count() == daysInWeek)
            {
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
            else
            {
                var viewModel = new HoursOfOperationViewModel
                {
                    Bar = bar,
                    DayOfWeeks = daysOfWeek,
                };
                return View(viewModel);
            }
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

            return RedirectToAction("BarDashboard");
        }



    }
}