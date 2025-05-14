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

                return View(model);
            }

            if (tab == "Email")
            {
                // Handle email view  here
            }

            if (tab == "Password")
            {
                // Handle password view  here
            }

            if (tab == "Delete")
            {
                // Handle deletion view here
            }

            return View(); // sends view with no model

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile()
        {
            // handle profile update logic here
            return View();
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
        public ActionResult Delete()
        {
            // handle account deletion logic here
            return View();
        }



    }
}