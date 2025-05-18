using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace JobApplication.Controllers
{

    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;

            string userId = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            string userName = identity.FindFirst(ClaimTypes.Name)?.Value;
            string email = identity.FindFirst(ClaimTypes.Email)?.Value;
            var role = identity.FindFirst(ClaimTypes.Role)?.Value;

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

        public ActionResult SetLanguage(string lang)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                HttpCookie langCookie = new HttpCookie("lang", lang);
                langCookie.Expires = DateTime.Now.AddYears(1);
                Response.Cookies.Add(langCookie);
            }

            string returnUrl = Request.UrlReferrer?.ToString() ?? Url.Action("Index", "Home");
            return Redirect(returnUrl);
        }
    }
}