using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace AccountTrackerWebApp
{
    public partial class Startup
    {
        private void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/UserAccount/SignIn"),
                Provider = new CookieAuthenticationProvider(),
                CookieSecure = CookieSecureOption.Always,
                CookieHttpOnly = true //Only allow cookies to be handled by HTTP (i.e., don't allow client-side JavaScript to interact with the cookie).
            });
        }
    }
}