using Course.Catalog.API.Features.Courses.Create;
using Course.Catalog.API.Features.Courses.Delete;
using Course.Catalog.API.Features.Courses.GetAll;
using Course.Catalog.API.Features.Courses.GetById;
using Course.Catalog.API.Features.Courses.Update;

namespace Course.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseEndpointsExtension(this WebApplication app)
        {
            var group = app.MapGroup("/api/courses").WithTags("Courses").
                CreateCourseGroupItemEndpoint().
                GetCourseByIdGroupItemEndpoint().
                GetAllCourseGroupItemEndpoint().
                UpdateCourseGroupItemEndpoint().
                DeleteCourseGroupItemEndpoint();
        }
    }
}
