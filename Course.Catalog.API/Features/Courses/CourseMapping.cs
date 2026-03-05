using Course.Catalog.API.Features.Courses.Create;

namespace Course.Catalog.API.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
        }
    }
}
