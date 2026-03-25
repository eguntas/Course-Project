using Asp.Versioning.Builder;
using Course.Discount.API.Features.Discounts.CreateDiscount;
using Course.Discount.API.Features.Discounts.GetDiscountByCode;

namespace Course.Discount.API.Features.Discounts
{
    public static class DiscountEndpointExtension
    {
        public static void AddDiscountEndpointExtension(this WebApplication app , ApiVersionSet apiVersion)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/discounts").WithTags("Discounts").WithApiVersionSet(apiVersion).
                CreateDiscountGroupItemEndpoint()
                .GetDiscountByCodeGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}
