namespace Course.Catalog.API.Features.Courses.Update
{
    public record UpdateCourseCommand(Guid id , string Name , string Description, string ImageUrl , decimal Price , Guid CategoryId):IRequestByServiceResult;
}
