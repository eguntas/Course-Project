namespace Course.Catalog.API.Features.Courses.Create
{
    public record CreateCourseCommand: IRequestByServiceResult<Guid>
    {
        public string Name { get; init; } = null!;
        public string Description { get; init; } = null!;
        public IFormFile? Picture { get; init; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; init; }

    }

}
