using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demo_Shop.Startup))]
namespace Demo_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
