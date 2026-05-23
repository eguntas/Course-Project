using Course.Web.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Refit;

namespace Course.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Get("/api/v1/courses")]
        Task<ApiResponse<List<CourseDto>>> GetAllCourses();

        [Get("/api/v1/courses/{id}")]
        Task<ApiResponse<CourseDto>> GetCourse(Guid id);

        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();

        [Get("/api/v1/courses/user/{userId}")]
        Task<ApiResponse<List<CourseDto>>> GetCoursesByUserIdAsync(Guid userId);

        [Multipart]
        [Post("/api/v1/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(
            [AliasAs("Name")] string Name ,
            [AliasAs("Description")] string Description ,
            [AliasAs("Price")] decimal Price ,
            [AliasAs("CategoryId")] string CategoryId ,
            [AliasAs("ImageFile")] StreamPart? Picture);

        [Put("/api/v1/courses")]
        Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest request);

        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid id);

        
    }
}
