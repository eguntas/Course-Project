using Course.Basket.API.Dtos;
using Course.Basket.API.Feature.Basket.DeleteBasketItem;
using Course.Shared;
using Course.Shared.Extensions;
using Course.Shared.Filters;
using Course.Shared.Services;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Text.Json;

namespace Course.Basket.API.Feature.Basket.RemoveDiscountCoupon
{
    public record RemoveDiscountCouponCommand: IRequestByServiceResult;


    public class RemoveDiscountCouponCommandHandler(BasketService basketService) : IRequestHandler<RemoveDiscountCouponCommand, ServiceResult>
     { 
        public async Task<ServiceResult> Handle(RemoveDiscountCouponCommand request, CancellationToken cancellationToken)
        {
           
            var basketAsJson = await basketService.GetBasketCacheKeyAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsJson))
            {
                return ServiceResult.Error("Basket not found", HttpStatusCode.NotFound);
            }
            var basket = System.Text.Json.JsonSerializer.Deserialize<Data.Basket>(basketAsJson);
            basket!.ClearDiscount();
            basketAsJson = JsonSerializer.Serialize(basket);
      

            await basketService.CreateBasketCacheKeyAsync(basket, cancellationToken);

            return ServiceResult.SuccessAsNoContent();


        }
    }

    public static class RemoveDiscountCouponEndpoint
    {
        public static RouteGroupBuilder RemoveDiscountCouponGroupItemEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapDelete("/remove-discount-coupon", async (IMediator mediator) =>
                (await mediator.Send(new RemoveDiscountCouponCommand())).ToGenericResult())
                .WithName("RemoveDiscountCoupon")
                .MapToApiVersion(1, 0);
             
            return builder;
        }
    }
}
