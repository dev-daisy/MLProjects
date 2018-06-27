using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HDRPortal.Startup))]
namespace HDRPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
