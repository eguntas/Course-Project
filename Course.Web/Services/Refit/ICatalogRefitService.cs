using Course.Web.Pages.Instructor.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Refit;

namespace Course.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Post("/api/v1/courses")]
        Task<ApiResponse<object>> CreateCourseAsync(CreateCourseRequest request);

        [Put("/api/v1/courses")]
        Task<ApiResponse<object>> UpdateCourseAsync(UpdateCourseRequest request);

        [Delete("/api/v1/courses/{id}")]
        Task<ApiResponse<object>> DeleteCourseAsync(Guid id);

        [Get("/api/v1/categories")]
        Task<ApiResponse<List<CategoryDto>>> GetCategoriesAsync();
    }
}
