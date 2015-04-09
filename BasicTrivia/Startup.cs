using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasicTrivia.Startup))]
namespace BasicTrivia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
