using Course.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace Course.Basket.API.Feature.Basket.ApplyDiscountCoupon
{
    public class ApplyDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<ApplyDiscountCouponCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(ApplyDiscountCouponCommand request, CancellationToken cancellationToken)
        {
            //var userId = Guid.NewGuid(); // This should be retrieved from the authenticated user context
            //var cacheKey = string.Format(Const.BasketConst.BasketCacheKey, userId);
            //var basketAsString = await cache.GetStringAsync(cacheKey, cancellationToken);
            var basketJson = await basketService.GetBasketCacheKeyAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }
            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketJson);
            if (!currentBasket.Items.Any())
            {
                return ServiceResult.Error("Basket item not found", HttpStatusCode.NotFound);

            }
            currentBasket.ApplyNewDiscount(request.Rate, request.Coupon);
            
            //await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(currentBasket), cancellationToken);
            await basketService.CreateBasketCacheKeyAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();


        }
    }
}
