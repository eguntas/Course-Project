using Course.Catalog.API.Features.Courses.Create;

namespace Course.Catalog.API.Features.Courses
{
    public static class CourseEndpointExtension
    {
        public static void AddCourseEndpointsExtension(this WebApplication app)
        {
            var group = app.MapGroup("/api/courses").WithTags("Courses").
                CreateCourseGroupItemEndpoint();
        }
    }
}
