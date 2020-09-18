using AccountTrackerLibrary.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace AccountTrackerLibrary.Security
{
    public class ApplicationSignInManager : SignInManager<User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager appUserManager, IAuthenticationManager authenticationManager)
            : base(appUserManager, authenticationManager)
        {
        }
    }
}
