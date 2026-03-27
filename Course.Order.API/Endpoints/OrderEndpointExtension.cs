using Asp.Versioning;
using Asp.Versioning.Builder;
using Course.Order.API.Endpoints.Orders;

namespace Course.Order.API.Endpoints
{
    public static class OrderEndpointExtension
    {
        public static void AddOrderEndpointsExtension(this WebApplication app , ApiVersionSet apiVersion)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/orders").WithTags("Orders").WithApiVersionSet(apiVersion)
               .CreateOrderGroupItemEndpoint()
               .GetOrdersGroupItemEndpoint().RequireAuthorization("Password");
        }
    }
}
