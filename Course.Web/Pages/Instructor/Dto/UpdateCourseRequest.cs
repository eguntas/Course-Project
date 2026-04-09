namespace Course.Web.Pages.Instructor.Dto
{
    public record UpdateCourseRequest(Guid id, string Name, string Description, string ImageUrl, decimal Price, Guid CategoryId);
   
}
