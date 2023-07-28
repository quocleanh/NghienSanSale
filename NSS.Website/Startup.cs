using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NSS.Website.Startup))]
namespace NSS.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
