using JobApplication.Models;
using JobApplication.Services.Interfaces;
using System.Web.Mvc;

namespace JobApplication.Controllers
{
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
        public ActionResult Index()
        {
            return View();
        }
    }
}