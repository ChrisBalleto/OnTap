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

        public ActionResult PatronViewOfBarDash(int id)
        {
            var currentUser = User.Identity.Name;
            var patron = _context.Patrons
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


    }
}