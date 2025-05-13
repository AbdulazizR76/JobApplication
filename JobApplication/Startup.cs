using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(JobApplication.Startup))]
namespace JobApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "CustomAppCookie",
                LoginPath = new PathString("/Account/Login"),
                LogoutPath = new PathString("/Account/Logout"),
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                SlidingExpiration = true,
            });


        }
    }
}
