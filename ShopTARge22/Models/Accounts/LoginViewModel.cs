using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace ShopTARge22.Models.Accounts
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name ="Remember me")]
        public bool RememberMe { get; set;}
        public string ReturnUrl { get; set; }

        //Google, FB-ga sisselogimiseks
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

    }
}
