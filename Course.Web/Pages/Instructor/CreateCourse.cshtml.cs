
using Course.Web.PageModel;
using Course.Web.Services;
using Course.Web.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService) : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        [BindProperty] public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
        public async Task OnGetAsync()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();

            if (categoriesResult.IsFailure)
            {
                //TODO : redirect error page
            }
            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await catalogService.CreateCourseAsync(ViewModel);
            
            if(!result.IsSuccess)
            {
                //TODO : redirect error page
            }
            return RedirectToPage("/Courses");
        }
    }
}
