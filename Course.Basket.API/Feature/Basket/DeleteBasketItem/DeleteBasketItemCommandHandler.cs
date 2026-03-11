using Course.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace Course.Basket.API.Feature.Basket.DeleteBasketItem
{
    public class DeleteBasketItemCommandHandler(BasketService basketService) : IRequestHandler<DeleteBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteBasketItemCommand request, CancellationToken cancellationToken)
        {
        
            var basketAsJson = await basketService.GetBasketCacheKeyAsync(cancellationToken);
            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }

            var currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);

            var basketItemToDelete = currentBasket.Items.FirstOrDefault(i => i.Id == request.Id);

            if (basketItemToDelete is null)
            {
                return ServiceResult.Error("Course not found in basket", HttpStatusCode.NotFound);
            }
            currentBasket.Items.Remove(basketItemToDelete);

            //basketAsJson = JsonSerializer.Serialize(currentBasket);
            //await cache.SetStringAsync(cacheKey, basketAsString, cancellationToken);
            await basketService.CreateBasketCacheKeyAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
