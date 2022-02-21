using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewFP.Startup))]
namespace NewFP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
