using Course.Web.Pages.Auth.SignIn;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Auth
{
    public class SignInModel(SignInService signInService) : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        [BindProperty] public required SignInViewModel SignInViewModel { get; set; } = SignInViewModel.ExampleModel;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var result = await signInService.SignInAsync(SignInViewModel);

            if (result.IsFailure)
            {
                ModelState.AddModelError(string.Empty, result.Fail!.Title!);

                ModelState.AddModelError(string.Empty, result.Fail!.Detail!);
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetSignOutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Index");
        }
    }
}
