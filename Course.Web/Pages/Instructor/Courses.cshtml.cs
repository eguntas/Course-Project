using Course.Web.Pages.Instructor.ViewModel;
using Course.Web.Services.Refit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Course.Web.Pages.Instructor
{
    public class CoursesModel(CatalogService catalogService) : PageModel
    {
        public List<CourseViewModel> courseViewModels { get; set; }
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
