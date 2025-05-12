using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JobApplication.Startup))]
namespace JobApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
