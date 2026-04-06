using Course.Web.Pages.Auth.SignUp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Auth
{
    public class SignUpModel(SignUpService service) : PageModel
    {
        [BindProperty] public SignUpViewModel SignUpViewModel { get; set; } = SignUpViewModel.Empty;
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var result = await service.CreateAccount(SignUpViewModel);
            if (result.IsFailure)
            {
                ModelState.AddModelError(string.Empty, result.Fail!.Title!);
                if(string.IsNullOrEmpty(result.Fail.Detail))
                    ModelState.AddModelError(string.Empty, result.Fail.Detail!);

                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
