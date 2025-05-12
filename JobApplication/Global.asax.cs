using Autofac;
using Autofac.Integration.Mvc;
using JobApplication.Models;
using Microsoft.AspNet.Identity.Owin;
using System.Reflection;
using System.Web;
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
            // Register DbContext (you can replace 'YourNamespace' with your actual namespace)
            builder.RegisterType<ApplicationDbContext>().AsSelf().InstancePerRequest();
            // Register other services if needed here...
            // Register UserManager from OWIN context
            builder.Register(c =>
                HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>())
                .AsSelf().InstancePerRequest();
            // Register SignInManager from OWIN context
            builder.Register(c =>
                HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>())
                .AsSelf().InstancePerRequest();

            var container = builder.Build();
            // Set MVC Dependency Resolver
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
