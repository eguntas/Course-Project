using AutoMapper;
using Course.Catalog.API.Features.Categories.Dtos;

namespace Course.Catalog.API.Features.Categories
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category , CategoryDto>().ReverseMap();
        }
    }
}
