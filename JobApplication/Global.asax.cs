using Autofac;
using Autofac.Integration.Mvc;
using JobApplication.Models;
using JobApplication.Services;
using JobApplication.Services.Interfaces;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace JobApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            // Register all controllers in the current assembly
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            // Register OWIN context
            builder.Register(c => HttpContext.Current.GetOwinContext()).AsSelf().InstancePerRequest();

            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            //builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<CryptoService>().As<ICryptoService>().InstancePerRequest();
            //here we are setting the Unique Identifier claim to our user 
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;


            var container = builder.Build();
            // Set MVC Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            string lang = HttpContext.Current.Request.Cookies["lang"]?.Value ?? "en";

            var culture = new System.Globalization.CultureInfo(lang);
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
