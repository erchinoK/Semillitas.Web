using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Semillitas.Web.Startup))]
namespace Semillitas.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
