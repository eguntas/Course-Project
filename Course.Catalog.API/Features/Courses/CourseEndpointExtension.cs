using Asp.Versioning;
using Asp.Versioning.Builder;
using Course.Catalog.API.Features.Courses.Create;
using Course.Catalog.API.Features.Courses.Delete;
using Course.Catalog.API.Features.Courses.GetAll;
using Course.Catalog.API.Features.Courses.GetAllByUserId;
using Course.Catalog.API.Features.Courses.GetById;
using Course.Catalog.API.Features.Courses.Update;

namespace Course.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseEndpointsExtension(this WebApplication app , ApiVersionSet apiVersion)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/courses").WithTags("Courses").WithApiVersionSet(apiVersion).
                CreateCourseGroupItemEndpoint().
                GetCourseByUserIdGroupItemEndpoint().
                GetAllCourseGroupItemEndpoint().
                UpdateCourseGroupItemEndpoint().
                DeleteCourseGroupItemEndpoint().
                GetCourseByIdGroupItemEndpoint();
        }
    }
}
