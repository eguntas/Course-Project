using Course.Web.Pages.Auth.SignUp;
using System.ComponentModel.DataAnnotations;

namespace Course.Web.Pages.Auth.SignIn
{
    public record SignInViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }

        public static SignInViewModel Empty =>
           new()
           {
               
               Email = string.Empty,
               Password = string.Empty
           };

        public static SignInViewModel ExampleModel =>
            new()
            {
                
                Email = "testuser@test.com",
                Password = "Password123"
            };
    }
}
