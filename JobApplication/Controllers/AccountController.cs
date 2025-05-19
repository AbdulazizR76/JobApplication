using JobApplication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobApplication.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;
        //private readonly IUserService _userService;
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;


        //public AccountController()
        //{
        //}

        public AccountController(ApplicationDbContext context, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
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
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}


            //// validate the user credentials
            //var result = _userService.ValidateUser(model.Email, model.Password);

            //if (!result.Success)
            //{
            //    if (result.CanReactivate)
            //    {
            //        _userService.RestoreAccount(result.User.Id);
            //        TempData["Success"] = "Your account has been reactivated. login please";
            //        return RedirectToAction("Login", "Account");
            //    }
            //    ModelState.AddModelError("", result.ErrorMessage);
            //    return View(model);
            //}

            //// if login successful, create claimsIdentity and sing in with owin 
            //var identity = new ClaimsIdentity("CustomAppCookie");
            //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.User.Id));
            //identity.AddClaim(new Claim(ClaimTypes.Email, result.User.Email));
            //identity.AddClaim(new Claim(ClaimTypes.Name, result.User.Name));


            ////foreach (var role in result.User.Roles.Select(ur => ur.Role.Name))
            ////{
            ////    identity.AddClaim(new Claim(ClaimTypes.Role, role));
            ////}

            //var authManager = HttpContext.GetOwinContext().Authentication;
            //authManager.SignIn(new AuthenticationProperties { IsPersistent = model.RememberMe }, identity);

            return RedirectToAction("Index", "Home");
        }




        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {

            // poplate deprtemnt list
            var department = new RegisterViewModel
            {
                DepartmentList = _context.Departments.Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }).ToList()
            };
            return View(department);
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.DepartmentList = _context.Departments.Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }).ToList();
                return View(model);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Position = model.Position,
                DepartmentId = model.DepartmentId,
            };

            var result = _userManager.Create(user, model.Password);
            if (result.Succeeded)
            {
                _signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }

            return View(model);


        }





        [HttpGet]
        public ActionResult LogOffGet()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut("CustomAppCookie");

            return RedirectToAction("Login", "Account");
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut("CustomAppCookie"); // must match your Startup.cs AuthenticationType

            return RedirectToAction("Login", "Account");

        }



    }
}