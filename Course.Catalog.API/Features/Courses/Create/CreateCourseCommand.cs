namespace Course.Catalog.API.Features.Courses.Create
{
    public record CreateCourseCommand(string Name , string Description , IFormFile? Picture, decimal Price , Guid CategoryId):IRequestByServiceResult<Guid>;

}
