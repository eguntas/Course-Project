namespace Course.Web.Pages.Instructor.Dto
{
    public record CourseDto(Guid Id, string Name, string Description, string ImageUrl, CategoryDto Category, FeatureDto Feature, decimal Price);
   
}
