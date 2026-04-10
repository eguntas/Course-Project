using Course.Web.Pages.Instructor.ViewModel;
using Course.Web.Services.Refit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Instructor
{
    [Authorize(Roles = "instructor")]
    public class CreateCourseModel(CatalogService catalogService) : PageModel
    {
        public CreateCourseViewModel ViewModel { get; set; } = CreateCourseViewModel.Empty;
        public async Task OnGet()
        {
            var categoriesResult = await catalogService.GetCategoriesAsync();

            if (categoriesResult.IsFailure)
            {
                //TODO : redirect error page
            }
            ViewModel.SetCategoryDropdownList(categoriesResult.Data!);
        }
    }
}
