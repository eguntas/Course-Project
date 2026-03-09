using Asp.Versioning.Builder;
using Course.Catalog.API.Features.Categories.Create;
using Course.Catalog.API.Features.Categories.GetAll;
using Course.Catalog.API.Features.Categories.GetById;

namespace Course.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryEndpointExtension(this WebApplication app , ApiVersionSet apiVersion)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/categories").WithTags("Categories").WithApiVersionSet(apiVersion).
                CreateCategoryGroupItemEndpoint().
                GetAllCategoryGroupItemEndpoint().
                GetByIdCategoryGroupItemEndpoint();
        }
    }
}
