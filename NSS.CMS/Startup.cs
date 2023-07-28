using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NSS.CMS.Startup))]
namespace NSS.CMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
