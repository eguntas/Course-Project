using Course.Web.PageModel;
using Course.Web.Services;
using Course.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages
{
    public class IndexModel(CatalogService catalogService, ILogger<IndexModel> logger) : BasePageModel
    {
        public List<CourseViewModel>? Courses { get; set; } = [];


        public async Task<IActionResult> OnGet()
        {
            var coursesAsResult = await catalogService.GetAllCoursesAsync();

            if (coursesAsResult.IsFailure) return ErrorPage(coursesAsResult);

            Courses = coursesAsResult.Data!;

            return Page();
        }
    }
}
