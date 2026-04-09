namespace Course.Web.Pages.Instructor.Dto
{
    public record CreateCourseRequest(string Name, string Description, IFormFile? Picture, decimal Price, Guid CategoryId);
  
}
