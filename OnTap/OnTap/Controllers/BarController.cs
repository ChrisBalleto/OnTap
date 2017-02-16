using OnTap.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZenOfBeer.BreweryDb;
using ZenOfBeer.BreweryDb.Pcl.Public;

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

        public ActionResult AddBeerToBar()
        {
            return View();
        }


    }
}