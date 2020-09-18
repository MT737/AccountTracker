using AccountTrackerLibrary.Models;
using AccountTrackerLibrary.Security;
using AccountTrackerWebApp.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccountTrackerWebApp.Controllers
{
    public class UserAccountController : Controller
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationSignInManager _signInManager;
        private readonly IAuthenticationManager _authenticationManager;
        
        public UserAccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            //Object not set to an instance of an object if a viewmodel is not passed.
            AccountRegistrationViewModel viewModel = new AccountRegistrationViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> Register(AccountRegistrationViewModel viewModel)
        {
            //If the ModelState is valid...
            if (ModelState.IsValid)
            {
                //Validate if the provided email is already in use.
                var existingUser = await _userManager.FindByEmailAsync(viewModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", $"The provided email address '{viewModel.Email}' has already been used to register an account. Please sign-in using your existing account.");
                }
                else
                {
                    //Instantiate a User object
                    User user = new User { UserName = viewModel.Email, Email = viewModel.Email };

                    //Create the user
                    var result = await _userManager.CreateAsync(user, viewModel.Password);

                    //If the user was successfully created...
                    if (result.Succeeded)
                    {
                        //Sign-in the user and redirect them to the web app's "Home" page
                        await _signInManager.SignInAsync(user, false, false);
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }

            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult SignIn()
        {
            //Object not set to an instance of an object if a viewmodel is not passed.
            AccountSignInViewModel viewModel = new AccountSignInViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> SignIn(AccountSignInViewModel viewModel)
        {
            //Check ModelState validity
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //Attemp user sign-in
            var result = await _signInManager.PasswordSignInAsync(viewModel.Email, viewModel.Password, viewModel.RememberMe, shouldLockout: false);

            //Check the result
            switch (result)
            {
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Success:
                    return RedirectToAction("Index", "Dashboard");

                case Microsoft.AspNet.Identity.Owin.SignInStatus.Failure:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(viewModel);

                case Microsoft.AspNet.Identity.Owin.SignInStatus.LockedOut:
                    //Should not be possible as the lockout feature is not enabled
                    throw new NotImplementedException("Identity feature not implemented.");

                case Microsoft.AspNet.Identity.Owin.SignInStatus.RequiresVerification:
                    throw new NotImplementedException("Identity feature not implemented.");

                default:
                    throw new Exception($"Unexpected Microsoft.AspNet.Identity.Owin.SignInStatus enum value: {result}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            
            return RedirectToAction("Index", "Dashboard");
        }
    }
}