using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using OnTap.Models;
using OnTap.ViewModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace OnTap.Controllers
{
    public class PatronController : Controller
    {
        private ApplicationDbContext _context;
        public PatronController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Patron
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditPatron()
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons.SingleOrDefault(c => c.Email == currentUser);

            if (patron == null)
                return HttpNotFound();

            var roleNames = _context.RoleNames.ToList();
            var cities = _context.Cities.ToList();
            var states = _context.States.ToList();
            var zipCodes = _context.ZipCodes.ToList();
            var viewModel = new RegisterPatronViewModel
            {
                Patron = patron,
                Cities = cities,
                RoleNames = roleNames,
                States = states,
                ZipCodes = zipCodes,
            };
            return View(viewModel);
        }

        public ActionResult FilterSearch(List<Bar> barz, SearchBarsViewModel viewModel)
        {
            List<Bar> filteredBars = new List<Bar>();

            var currentUser = User.Identity.Name;
            var patron = _context.Patrons
                .Include(r => r.BarReviews)
                .Include(p => p.FollowedBars)
                .Include(z => z.City)
                .Include(y => y.State)
                .Include(z => z.ZipCode)
                .SingleOrDefault(c => c.Email == currentUser);
            var bars = _context.Bars
            .Include(x => x.ZipCode)
                .Include(x => x.City)
                .Include(x => x.State)
                .Include(x => x.FeedMessages)
                .Include(x => x.BarGames)
                .Include(x => x.Specials)
                .Include(x => x.SportsPackages)
                .Include(x => x.TapBeers)
            .ToList();
            if (viewModel.SportsPackage.Id != 0)
            {
                foreach(var bar in bars.ToList())
                {
                    foreach (var package in bar.SportsPackages)
                    {
                        if (package.Id == viewModel.SportsPackage.Id)
                        {
                            filteredBars.Add(bar);
                        }
                    }
                }
            }
            if (viewModel.BarGame.Id != 0)
            {
                foreach (var bar in bars.ToList())
                {
                    foreach (var game in bar.BarGames)
                    {
                        if (game.Id == viewModel.BarGame.Id)
                        {
                            filteredBars.Add(bar);
                        }
                    }
                }
            }
            if (viewModel.TapBeer.Id != 0)
            {
                foreach (var bar in bars.ToList())
                {
                    foreach (var tap in bar.TapBeers)
                    {
                        if (tap.Id == viewModel.TapBeer.Id)
                        {
                            filteredBars.Add(bar);
                        }
                    }
                }
            }
            var barGames = _context.BarGames.ToList();
            var sportsPackages = _context.SportsPackages.ToList();
            var specials = _context.Specials.ToList();
            var tapBeers = _context.TapBeers.ToList();
            var states = _context.States.ToList();
            var zipCodes = _context.ZipCodes.ToList();
            var cities = _context.Cities.ToList();
            var daysOfWeek = _context.DayOfWeeks.ToList();
            var searchViewModel = new SearchBarsViewModel
            {
                Patron = patron,
                Bars = filteredBars,
                BarGames = barGames,
                SportsPackages = sportsPackages,
                TapBeers = tapBeers,
                States = states,
                Cities = cities,
                ZipCodes = zipCodes,
                Specials = specials,
            };




            return View("SearchBars", searchViewModel);
        }

        public ActionResult PatronViewOfBarDash(int id)
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons
                .Include(r => r.BarReviews)
                .Include(p => p.FollowedBars)
                .Include(z => z.City)
                .Include(y => y.State)
                .Include(z => z.ZipCode)
                .SingleOrDefault(c => c.Email == currentUser);
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
            .SingleOrDefault(c => c.Id == id);

            var viewModel = new PatronViewOfBarDashViewModel()
            {
                Patron = patron,
                Bar = bar,
                DayOfWeeks = _context.DayOfWeeks.ToList()
            };

            return View(viewModel);
        }



        public ActionResult PatronDashboard()
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons
                .Include(p => p.FollowedBars)
                .Include(z => z.City)
                .Include(y => y.State)
                .Include(z => z.ZipCode)
                .SingleOrDefault(c => c.Email == currentUser);
            var daysOfWeek = _context.DayOfWeeks.ToList();

            var viewModel = new PatronDashboardViewModel
            {
                Patron = patron,
                DaysofWeek = daysOfWeek,
                
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
            var barGames = _context.BarGames.Include(x => x.Bars).ToList();
            var sportsPackages = _context.SportsPackages.Include(x => x.Bars).ToList();
            var specials = _context.Specials.ToList();
            var tapBeers = _context.TapBeers.Include(x => x.Bars).ToList();
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
                Patron = patron,
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

        public ActionResult FollowBar(int id)
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons.Include(p => p.FollowedBars).SingleOrDefault(c => c.Email == currentUser);
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);

            patron.FollowedBars.Add(bar);
            bar.Followers.Add(patron);
            _context.SaveChanges();


            return RedirectToAction("SearchBars");
        }

        public ActionResult UnFollowBar(int id)
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons.Include(p => p.FollowedBars).SingleOrDefault(c => c.Email == currentUser);
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);

            patron.FollowedBars.Remove(bar);
            bar.Followers.Remove(patron);
            _context.SaveChanges();


            return RedirectToAction("SearchBars");
        }

        public ActionResult UnFollowBarFromDash(int id)
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons.Include(p => p.FollowedBars).SingleOrDefault(c => c.Email == currentUser);
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id);

            patron.FollowedBars.Remove(bar);
            bar.Followers.Remove(patron);
            _context.SaveChanges();


            return RedirectToAction("PatronDashboard");
        }

        [HttpPost]
        public ActionResult BarReview(PatronViewOfBarDashViewModel viewModel)
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons.SingleOrDefault(p => p.Email == currentUser);
            var bar = _context.Bars.Include(x => x.BarReviews).SingleOrDefault(c => c.Id == viewModel.Bar.Id);
             

            var review = new Review()
            {
                Patron = patron,
                Bar = bar,
                Content = viewModel.Review.Content,
                Created = DateTime.Now,
                Subject = viewModel.Review.Subject,
                Rating = viewModel.Review.Rating,
            };
            bar.BarReviews.Add(review);
            patron.BarReviews.Add(review);
            if (bar.BarReviews.Count == 0)
            {
                bar.Rating = 0;
            }
            else
            {
                double count = 0;
                foreach(var rating in bar.BarReviews)
                {
                    count = count + rating.Rating;
                }
                double reviews = bar.BarReviews.Count;
                var average = count / reviews;
                bar.Rating = Math.Round(average, 2);
            }
            
            _context.SaveChanges();

            var id = viewModel.Bar.Id;

            return RedirectToAction("PatronViewOfBarDash", new { Id = id });
        }
        [HttpDelete]
        public ActionResult RemoveReview(int id, int id2)
        {
            var currentuser = User.Identity.Name;
            var patron = _context.Patrons.SingleOrDefault(p => p.Email == currentuser);
            var bar = _context.Bars.SingleOrDefault(c => c.Id == id2);
            var review = _context.Reviews.SingleOrDefault(c => c.Id == id);
            _context.Reviews.Remove(review);
            if (bar.BarReviews.Count == 0 )
            {
                bar.Rating = 0;
            }
            else
            {
                double count = 0;
                foreach (var rating in bar.BarReviews)
                {
                    count = count + rating.Rating;
                }
                double reviews = bar.BarReviews.Count;
                var average = count / reviews;
                bar.Rating = Math.Round(average, 2);
            }

            _context.SaveChanges();

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("PatronViewOfBarDash", "Patron", new { Id = id2 });
            return Json(new { Url = redirectUrl });
        }

    }
}