using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
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

        public ActionResult PatronDashboard(Patron patron)
        {
            return View();
        }
        

        
    }
}