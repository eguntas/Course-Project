namespace Course.Web.Pages.Instructor.Dto
{
    public record FeatureDto()
    {
        public int Duration { get; set; }
        public float Rating { get; set; }
        public string EducatorFullName { get; set; } = default!;
    }
}
