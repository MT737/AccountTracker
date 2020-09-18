using System.ComponentModel.DataAnnotations;

namespace AccountTrackerWebApp.ViewModels
{
    public class AccountSignInViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}