using JobApplication.Models;
using JobApplication.Services.Interfaces;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace JobApplication.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        // GET: Manage
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;

        public ManageController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;

        }
        public ActionResult Index(string tab = "Profile")
        {
            ViewBag.ActiveTab = tab;
            var identity = (ClaimsIdentity)User.Identity;
            string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (tab == "Profile")
            {
                var user = _userService.GetUserById(userId);
                if (user == null)
                {
                    return HttpNotFound();
                }

                var model = new EditProfileViewModel
                {
                    UserId = userId,
                    Name = user.Name,
                    Email = user.Email,
                    Position = user.Position,
                    DepartmentId = user.DepartmentId,
                    // need to have eager loading for departments
                    Departments = _context.Departments.Select(d => new SelectListItem
                    {
                        Text = d.Name,
                        Value = d.Id.ToString()
                    }).ToList()
                };
                ViewBag.ActiveTab = "Profile";
                return View(model);
            }

            if (tab == "Email")
            {
                // Handle email view  here
            }

            if (tab == "Password")
            {
                var model = new ChangePasswordViewModel
                {
                    UserId = userId
                };
                // Handle password view  here
                ViewBag.ActiveTab = "Password";
                return View(model); // or return View(model);
            }

            if (tab == "Delete")
            {
                // Handle deletion view here
            }

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(EditProfileViewModel model)
        {
            // handle profile update logic here
            model.Departments = _context.Departments.Select(d => new SelectListItem
            {
                Text = d.Name,
                Value = d.Id.ToString()
            }).ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.ActiveTab = "Profile";
                return View("Index", model);

            }
            _userService.UpdateProfile(model);
            TempData["Success"] = "Profile updated!";
            return RedirectToAction("Index", new { tab = "Profile" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateEmail()
        {
            // handle email update logic here
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePasswrod(ChangePasswordViewModel model)
        {
            // handle email update logic here
            if (!ModelState.IsValid)
            {
                ViewBag.ActiveTab = "Password";
                return View("Index", model);
            }

            var result = _userService.ChangePassword(model);
            if (result.Success)
            {
                TempData["Success"] = result.ErrorMessage;
                return RedirectToAction("Index", new { tab = "Password" });
            }
            else
            {
                ModelState.AddModelError("", result.ErrorMessage);
                ViewBag.ActiveTab = "Password";
                return View("Index", model);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete()
        {
            // handle account deletion logic here
            return View();
        }





    }
}