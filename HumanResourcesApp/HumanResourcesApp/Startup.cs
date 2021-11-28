using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HumanResourcesApp.Startup))]
namespace HumanResourcesApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
