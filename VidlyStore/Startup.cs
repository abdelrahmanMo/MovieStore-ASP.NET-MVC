using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VidlyStore.Startup))]
namespace VidlyStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
