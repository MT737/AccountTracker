using AccountTrackerLibrary.Models;
using Microsoft.AspNet.Identity;

namespace AccountTrackerLibrary.Security
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> userStore)
            : base(userStore)
        {
        }
    }
}
