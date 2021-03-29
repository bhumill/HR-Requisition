using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRRequisition.Startup))]
namespace HRRequisition
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
