using Course.Catalog.API.Features.Courses.Create;
using Course.Catalog.API.Features.Courses.Dtos;

namespace Course.Catalog.API.Features.Courses
{
    public class CourseMapping:Profile
    {
        public CourseMapping()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<Course , CourseDto>().ReverseMap();
            CreateMap<Feature , FeatureDto>().ReverseMap();

        }
    }
}
