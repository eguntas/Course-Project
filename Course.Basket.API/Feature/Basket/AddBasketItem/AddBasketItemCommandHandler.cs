using Course.Shared;
using Course.Shared.Services;
using MediatR;
using System.Text.Json;

namespace Course.Basket.API.Feature.Basket.AddBasketItem
{
    public class AddBasketItemCommandHandler(IIdentityService identityService , BasketService basketService) : IRequestHandler<AddBasketItemCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(AddBasketItemCommand request, CancellationToken cancellationToken)
        {
          
            var basketAsJson = await basketService.GetBasketCacheKeyAsync(cancellationToken);

            var newBasketItem = new Data.BasketItem(request.CourseId, request.CourseName, request.ImageUrl, request.CoursePrice,null);

            Data.Basket currentBasket;
            if (string.IsNullOrEmpty(basketAsJson))
            {
                currentBasket = new Data.Basket(identityService.GetUserId, [newBasketItem]);
                await basketService.CreateBasketCacheKeyAsync(currentBasket, cancellationToken);
                return ServiceResult.SuccessAsNoContent();
            }
            
             currentBasket = JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
             var existBasketItem = currentBasket.Items.FirstOrDefault(x => x.Id == request.CourseId);

             if (existBasketItem is not null)
             {
                 currentBasket.Items.Remove(existBasketItem);
                
             }


            currentBasket.Items.Add(newBasketItem);
            currentBasket.ApplyAvaibleDiscount();
            await basketService.CreateBasketCacheKeyAsync(currentBasket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
       
    }
}
