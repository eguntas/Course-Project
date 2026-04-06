using System.ComponentModel.DataAnnotations;

namespace Course.Web.Pages.Auth.SignUp
{
    public class SignUpViewModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "User Name is required")]
        public string Username { get; init; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "Firs Name is required")]
        public string FirstName { get; init; }

        [Display(Name = "Last name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; init; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; init; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; init; }

        [Display(Name = "Password Confirm")]
        [Required(ErrorMessage = "Password Confirm is required")]
        [Compare(nameof(Password), ErrorMessage = "The Password don't match")]
        public string PasswordConfirm { get; init; }

        public static SignUpViewModel Empty =>
            new()
            {
                Username = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty,
                Email = string.Empty,
                Password = string.Empty,
                PasswordConfirm = string.Empty
            };

        public static SignUpViewModel ExampleMdodel =>
            new()
            {
                Username = "TestUser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@test.com",
                Password = "Password123",
                PasswordConfirm = "Password123"
            };
    }
}
