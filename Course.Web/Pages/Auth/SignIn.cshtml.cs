using Course.Web.Pages.Auth.SignIn;
using Course.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Auth
{
    public class SignInModel : PageModel
    {
        [BindProperty] public SignInViewModel SignInViewModel { get; set; } = SignInViewModel.ExampleModel;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync([FromServices] SignInService signInService)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await signInService.SignInAsync(SignInViewModel);
            if (result.IsFailure)
            {
                ModelState.AddModelError(string.Empty, result.Fail.Title);
                ModelState.AddModelError(string.Empty, result.Fail.Detail);
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
