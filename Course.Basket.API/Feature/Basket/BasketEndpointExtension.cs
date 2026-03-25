using Asp.Versioning.Builder;
using Course.Basket.API.Feature.Basket.AddBasketItem;
using Course.Basket.API.Feature.Basket.ApplyDiscountCoupon;
using Course.Basket.API.Feature.Basket.DeleteBasketItem;
using Course.Basket.API.Feature.Basket.GetBasket;
using Course.Basket.API.Feature.Basket.RemoveDiscountCoupon;

namespace Course.Basket.API.Feature.Basket
{
    public static class BasketEndpointExtension
    {
        public static void AddBasketEndpointExtension(this WebApplication app, ApiVersionSet apiVersion)
        {
            app.MapGroup("/api/v{version:apiVersion}/baskets").WithTags("Baskets").WithApiVersionSet(apiVersion)
                .AddBasketItemGroupItemEndpoint()
                .DeleteBasketItemGroupItemEndpoint()
                .GetBasketGroupItemEndpoint()
                .ApplyDiscountCouponItemGroupItemEndpoint()
                .RemoveDiscountCouponGroupItemEndpoint()
                .RequireAuthorization();
        }
    }
}
