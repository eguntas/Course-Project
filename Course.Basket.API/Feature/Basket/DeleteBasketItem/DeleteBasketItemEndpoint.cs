using Course.Basket.API.Feature.Basket.AddBasketItem;
using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;

namespace Course.Basket.API.Feature.Basket.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapDelete("/item/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteBasketItemCommand(id));
                return result.ToGenericResult();
            }).WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<DeleteBasketItemCommandValidator>>();

            return builder;
        }
    }
}
