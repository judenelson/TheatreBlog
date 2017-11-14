using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TheatreBlog.Startup))]
namespace TheatreBlog
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
