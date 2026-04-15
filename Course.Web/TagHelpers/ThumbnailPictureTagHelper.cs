using Course.Web.Options;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Course.Web.TagHelpers
{
    public class ThumbnailPictureTagHelper(MicroserviceOption microserviceOption):TagHelper
    {
        public string? Src { get; set; }
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            var blankCourseImage = "/images/blank-course-image.png";

            if (string.IsNullOrEmpty(Src))
            {
                output.Attributes.SetAttribute("src", blankCourseImage);
            }
            else
            {
                var courseImage = $"{microserviceOption.File.BaseAddress}/{Src}";
                output.Attributes.SetAttribute("src", courseImage);
            }

            return base.ProcessAsync(context, output);
        }
    }
}
