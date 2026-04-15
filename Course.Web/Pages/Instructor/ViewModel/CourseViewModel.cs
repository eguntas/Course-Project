namespace Course.Web.Pages.Instructor.ViewModel
{
    public record CourseViewModel(Guid Id, string Name, string Description, string ImageUrl, decimal Price , string CategoryName , int Duration , float Rating)
    {
        public string TruncateDescription(int maxLength = 100)
        {
            if (Description.Length <= maxLength)
                return Description;
            return Description.Substring(0, maxLength) + "...";
        }
    }
    
}
