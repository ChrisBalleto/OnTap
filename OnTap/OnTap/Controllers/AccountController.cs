﻿using System;
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
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _context;

        public AccountController()
        {
            _context = new ApplicationDbContext();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _context = new ApplicationDbContext();
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
       

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        
        [AllowAnonymous]
        public ActionResult ChooseRole()
        {
            var _context = new ApplicationDbContext();
            var roleNames = _context.RoleNames.ToList();
            var viewModel = new RoleViewModel
            {
                RoleNames = roleNames
            };
            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChooseRole(RoleViewModel model)
        {
            if (model.RoleNameId == 1)
                return RedirectToAction("RegisterPatron", model);

            if (model.RoleNameId == 2)
                return RedirectToAction("RegisterBar", model);


            return View();
        }

        public ActionResult EditBar()
        {
            return View();
        }
       
        [AllowAnonymous]
        public ActionResult RegisterBar(RoleViewModel model)
        {
            var roleNames = _context.RoleNames.ToList();
            var cities = _context.Cities.ToList();
            var states = _context.States.ToList();
            var zipCodes = _context.ZipCodes.ToList();
            var viewModel = new RegisterBarViewModel
            {
                Bar = new Bar(),
                Cities = cities,
                RoleNames = roleNames,
                States = states,
                ZipCodes = zipCodes,
            };
            viewModel.Bar.RoleNameId = model.RoleNameId;

            return View(viewModel);
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterBar(Bar bar)
        {
            if (ModelState.IsValid && bar.Id == 0)
            {
                var user = new ApplicationUser { UserName = bar.Email, Email = bar.Email };
                var result = await UserManager.CreateAsync(user, bar.Password);
                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("Bar"));
                    await UserManager.AddToRoleAsync(user.Id, "Bar");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    
                    _context.Bars.Add(bar);
                }               
                AddErrors(result);
            }
            else if (!ModelState.IsValid)
            {
                var viewModel = new RegisterBarViewModel
                {
                    Bar = bar,
                    ZipCodes = _context.ZipCodes.ToList(),
                    Cities = _context.Cities.ToList(),
                    States = _context.States.ToList(),
                    RoleNames = _context.RoleNames.ToList(),
                };
                return View("RegisterBar", viewModel);
            }
            else if (bar.Id != 0)
            {
                var barInDb = _context.Bars.Single(c => c.Id == bar.Id);
                barInDb.Email = bar.Email;
                barInDb.BarName = bar.BarName;
                barInDb.StreetOne = bar.StreetOne;
                barInDb.StreetTwo = bar.StreetTwo;
                barInDb.StateId = bar.StateId;
                barInDb.CityId = bar.CityId;
                barInDb.ZipCodeId = bar.ZipCodeId;
                barInDb.PhoneNumber = bar.PhoneNumber;
                barInDb.HasJukebox = bar.HasJukebox;
                barInDb.HasWifi = bar.HasWifi;
                barInDb.BarReviews = bar.BarReviews;
                barInDb.Specials = bar.Specials;
                barInDb.BarGames = bar.BarGames;
                barInDb.FeedMessages = bar.FeedMessages;
                barInDb.TapBeers = bar.TapBeers;
                barInDb.SportsPackages = bar.SportsPackages;
                barInDb.HoursOfOperations = bar.HoursOfOperations;
                barInDb.Rating = bar.Rating;
                barInDb.RatingCount = bar.RatingCount;
            }
            
            

            _context.SaveChanges();
            return RedirectToAction("BarDashboard", "Bar");
        }

        [AllowAnonymous]
        public ActionResult RegisterPatron(RoleViewModel model)
        {
            var roleNames = _context.RoleNames.ToList();
            var cities = _context.Cities.ToList();
            var states = _context.States.ToList();
            var zipCodes = _context.ZipCodes.ToList();
            var viewModel = new RegisterPatronViewModel
            {
                Patron = new Patron(),
                Cities = cities,
                RoleNames = roleNames,
                States = states,
                ZipCodes = zipCodes,
            };
            viewModel.Patron.RoleNameId = model.RoleNameId;

            return View(viewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterPatron(Patron patron)
        {
            if (ModelState.IsValid && patron.Id == 0)
            {
                var user = new ApplicationUser { UserName = patron.Email, Email = patron.Email };
                var result = await UserManager.CreateAsync(user, patron.Password);
                if (result.Succeeded)
                {
                    var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
                    var roleManager = new RoleManager<IdentityRole>(roleStore);
                    await roleManager.CreateAsync(new IdentityRole("Patron"));
                    await UserManager.AddToRoleAsync(user.Id, "Patron");
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    _context.Patrons.Add(patron);

                }
                
                AddErrors(result);
            }
            else if (patron.Id != 0)
            {
                var patronInDb = _context.Patrons.Single(c => c.Id == patron.Id);
                patronInDb.FirstName = patron.FirstName;
                patronInDb.LastName = patron.LastName;
                patronInDb.BirthDate = patron.BirthDate;
                patronInDb.StreetOne = patron.StreetOne;
                patronInDb.StreetTwo = patron.StreetTwo;
                patronInDb.StateId = patron.StateId;
                patronInDb.CityId = patron.CityId;
                patronInDb.ZipCodeId = patron.ZipCodeId;
                patronInDb.PhoneNumber = patron.PhoneNumber;
                patronInDb.FollowedBars = patron.FollowedBars;
                patronInDb.BarReviews = patron.BarReviews;
                patronInDb.ParsedAddress = (patron.StreetOne + patron.StreetTwo).Replace(" ", "+");
            }
            else if (!ModelState.IsValid)
            {
                var viewModel = new RegisterPatronViewModel
                {
                    Patron = patron,
                    ZipCodes = _context.ZipCodes.ToList(),
                    Cities = _context.Cities.ToList(),
                    States = _context.States.ToList(),
                    RoleNames = _context.RoleNames.ToList(),
                };
                return View("RegisterPatron", viewModel);
            }
            
            _context.SaveChanges();
            return RedirectToAction("PatronDashboard", "Patron");
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}