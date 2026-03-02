using Course.Catalog.API.Repositories;

namespace Course.Catalog.API.Features.Courses
{
    public class Feature
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
