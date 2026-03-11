using Course.Basket.API.Feature.Basket.AddBasketItem;
using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;

namespace Course.Basket.API.Feature.Basket.ApplyDiscountCoupon
{
    public static class ApplyDiscountCouponEndpoint
    {
        public static RouteGroupBuilder ApplyDiscountCouponItemGroupItemEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPut("/apply-discount-rate", async (ApplyDiscountCouponCommand command, IMediator mediator) =>
            {
                var result = await mediator.Send(command);
                return result.ToGenericResult();
            }).WithName("ApplyDiscountRate")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<ApplyDiscountCouponCommandValidator>>();

            return builder;
        }
    }
}
