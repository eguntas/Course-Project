using Course.Catalog.API.Repositories;
using Course.Catalog.API.Features.Courses;

namespace Course.Catalog.API.Features.Categories
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = default!;
        public List<Course.Catalog.API.Features.Courses.Course>? Courses  { get; set; } = default!;
    }
}
