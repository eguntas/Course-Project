using Course.Web.Extensions;
using Course.Web.ViewModel;
using Course.Web.Services.Refit;
using Refit;
using System.Text.Json;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Course.Web.Services
{
    public class CatalogService(ICatalogRefitService catalogRefitService, UserService userService, ILogger<CatalogService> logger)
    {

        public async Task<ServiceResult<List<CourseViewModel>>> GetAllCoursesAsync()
        {
            var coursesAsResult = await catalogRefitService.GetAllCourses();

            if (!coursesAsResult.IsSuccessStatusCode)
            {
                logger.LogProblemDetails(coursesAsResult.Error);

                return ServiceResult<List<CourseViewModel>>.Error(
                    "Failed to retrieve course data. Please try again later.");
            }


            var courses = coursesAsResult.Content!;

            var categoriesViewModel = courses.Select(c =>
                new CourseViewModel(c.Id, c.Name, c.Description, c.Price, c.ImageUrl, c.Created.ToLongDateString(),
                    c.Feature.EducatorFullName, c.Category.Name, c.Feature.Duration,
                    c.Feature.Rating)).ToList();

            return ServiceResult<List<CourseViewModel>>.Success(categoriesViewModel);
        }


        public async Task<ServiceResult<CourseViewModel>> GetCourse(Guid courseId)
        {
            var response = await catalogRefitService.GetCourse(courseId);

            if (!response.IsSuccessStatusCode)
                return ServiceResult<CourseViewModel>.FailFromProblemDetails(response.Error);


            var course = response.Content!;
            var courseViewModel = new CourseViewModel(course.Id, course.Name, course.Description, course.Price,
                course.ImageUrl, course.Created.ToLongDateString(), course.Feature.EducatorFullName, course.Category.Name,
                course.Feature.Duration,
                course.Feature.Rating);

            return ServiceResult<CourseViewModel>.Success(courseViewModel);
        }
        public async Task<ServiceResult<List<CategoryViewModel>>> GetCategoriesAsync()
        {
            var response = await catalogRefitService.GetCategoriesAsync();
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while fetching categories");
                return ServiceResult<List<CategoryViewModel>>.Error("Fail to retrieve categories. Please try again later");
            }

            var categories = response!.Content!.Select(c => new CategoryViewModel(c.Id, c.Name))
                .ToList();
            return ServiceResult<List<CategoryViewModel>>.Success(categories);
        }

        public async Task<ServiceResult> CreateCourseAsync(CreateCourseViewModel model)
        {
            StreamPart? picture = null;
            await using var stream = model.PictureFormFile?.OpenReadStream();

            if (model.PictureFormFile is not null && model.PictureFormFile.Length > 0)
            {
                picture = new StreamPart(stream!, model.PictureFormFile.FileName, model.PictureFormFile.ContentType);
            }

            var response = await catalogRefitService.CreateCourseAsync(model.Name, model.Description, model.Price, model.CategoryId.ToString()!, picture);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while creating course");
                return ServiceResult.Error("Fail to create course. Please try again later");
            }
            return ServiceResult.Success();
        }

        public async Task<ServiceResult<List<CourseViewModel>>> GetCoursesByUserIdAsync()
        {
            var course = await catalogRefitService.GetCoursesByUserIdAsync(userService.GetUserId);

            if (!course.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(course.Error.Content!);
                logger.LogError("Error occurred while fetching courses for user {UserId}", userService.GetUserId);
                return ServiceResult<List<CourseViewModel>>.Error("Fail to retrieve courses. Please try again later");
            }
            var courses = course!.Content!
           .Select(c => new CourseViewModel(
               c.Id,
               c.Name,
               c.Description,
               c.Price,
               c.ImageUrl,
               c.Created.ToLongDateString(),
               c.Feature.EducatorFullName,
               c.Category.Name,
               c.Feature.Duration,
               c.Feature.Rating
           ))
           .ToList();

            return ServiceResult<List<CourseViewModel>>.Success(courses);
        }

        public async Task<ServiceResult> DeleteAsync(Guid courseId)
        {
            var response = await catalogRefitService.DeleteCourseAsync(courseId);
            if (!response.IsSuccessStatusCode)
            {
                var problemDetails = JsonSerializer.Deserialize<Microsoft.AspNetCore.Mvc.ProblemDetails>(response.Error.Content!);
                logger.LogError("Error occurred while deleting course with id {CourseId}", courseId);
                return ServiceResult.Error("Fail to delete course. Please try again later");
            }
            return ServiceResult.Success();
        }
    }
}
