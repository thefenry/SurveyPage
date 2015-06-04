using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SurveyPage.Startup))]
namespace SurveyPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
