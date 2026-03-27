using Course.Shared.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Course.Basket.API.Feature.Basket
{
    public class BasketService(IIdentityService identityService, IDistributedCache cache)
    {
        private string GetCacheKey() => string.Format(Const.BasketConst.BasketCacheKey, identityService.GetUserId);

        private string GetCacheKey(Guid userId) => string.Format(Const.BasketConst.BasketCacheKey, userId);


        public Task<string?> GetBasketCacheKeyAsync(CancellationToken cancellationToken)
        {
            return cache.GetStringAsync(GetCacheKey(), cancellationToken);
        }

        public Task CreateBasketCacheKeyAsync(Data.Basket basket, CancellationToken cancellationToken)
        {
            var basketAsString = System.Text.Json.JsonSerializer.Serialize(basket);
            return cache.SetStringAsync(GetCacheKey(), basketAsString, cancellationToken);
        }

        public async Task DeleteBasket(Guid userId)
        {
            await cache.RemoveAsync(GetCacheKey(userId));
        }
    }
}
