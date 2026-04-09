using Course.Web.Pages.Instructor.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Refit;

namespace Course.Web.Services.Refit
{
    public interface ICatalogRefitService
    {
        [Post("/v1/catalog/courses")]
        Task<ApiResponse<ServiceResult>> CreateCourseAsync(CreateCourseRequest request);

        [Put("/v1/catalog/courses")]
        Task<ApiResponse<ServiceResult>> UpdateCourseAsync(UpdateCourseRequest request);

        [Delete("/v1/catalog/courses/{id}")]
        Task<ApiResponse<ServiceResult>> DeleteCourseAsync(Guid id);
    }
}
