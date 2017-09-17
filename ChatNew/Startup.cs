using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatNew.Startup))]
namespace ChatNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
