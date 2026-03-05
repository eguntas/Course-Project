using Course.Catalog.API.Features.Categories.Create;
using Course.Catalog.API.Features.Categories.GetAll;
using Course.Catalog.API.Features.Categories.GetById;

namespace Course.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryEndpointExtension(this WebApplication app)
        {
            var group = app.MapGroup("/api/categories").WithTags("Categories").
                CreateCategoryGroupItemEndpoint().
                GetAllCategoryGroupItemEndpoint().
                GetByIdCategoryGroupItemEndpoint();
        }
    }
}
