using Course.Catalog.API.Features.Categories.Create;

namespace Course.Catalog.API.Features.Categories
{
    public static class CategoryEndpointExtension
    {
        public static void AddCategoryEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/categories").CreateCategoryGroupItemEndpoint();
        }
    }
}
