using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Waterval.Startup))]
namespace Waterval
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
