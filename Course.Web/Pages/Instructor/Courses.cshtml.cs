using Course.Web.Services;
using Course.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        public List<CourseViewModel> courseViewModels { get; set; } = null!;
        public async Task OnGetAsync()
        {
            var result = await catalogService.GetCoursesByUserIdAsync();

            if (result.IsFailure)
            {
                //TODO: Log error
            }
            
            courseViewModels = result.Data!;
            
        }
        public async Task<IActionResult> OnGetDeleteAsync(Guid id)
        {
            var result = await catalogService.DeleteAsync(id);

            if (result.IsFailure)
            {
                //TODO: Log error
            }

            return RedirectToPage();
        }
    }
}
