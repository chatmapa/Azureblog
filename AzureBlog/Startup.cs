using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AzureBlog.Startup))]
namespace AzureBlog
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
