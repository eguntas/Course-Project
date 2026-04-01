using System.ComponentModel.DataAnnotations;

namespace Course.Web.Pages.Auth.SignUp
{
    public record SignUpViewModel(
        [Display(Name ="Username" )] string Username ,
        [Display(Name = "First name")] string FirstName ,
        [Display(Name = "Last name")] string LastName ,
        [Display(Name = "Email")] string Email ,
        [Display(Name = "Password")] string Password ,
        [Display(Name = "Password Confirm")] string PasswordConfirm)
    {
        public static SignUpViewModel Empty => new(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);

        public static SignUpViewModel ExampleMdodel => new("TestUser", "Test", "User", "testuser@test.com", "Password123", "Password123");
    }


    
}
