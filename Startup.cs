using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PGProgrammeApplications.Startup))]
namespace PGProgrammeApplications
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
