namespace Course.Catalog.API.Features.Courses.Dtos
{
    public record FeatureDto()
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
