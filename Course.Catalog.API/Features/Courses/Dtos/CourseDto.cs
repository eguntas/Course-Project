using Course.Catalog.API.Features.Categories.Dtos;

namespace Course.Catalog.API.Features.Courses.Dtos
{
    public class CourseDto(Guid Id, string Name, string Description, string ImageUrl, CategoryDto Category, FeatureDto Feature, decimal Price);
   
}
