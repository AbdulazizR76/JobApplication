using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace JobApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var userId = User.Identity.GetUserId();
            ViewBag.username = username;
            ViewBag.userId = userId;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}