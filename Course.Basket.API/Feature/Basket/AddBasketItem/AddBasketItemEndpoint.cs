using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;

namespace Course.Basket.API.Feature.Basket.AddBasketItem
{
    public static class AddBasketItemEndpoint
    {
        public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .WithName("AddBasketItem")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<AddBasketItemCommandValidator>>();

            return builder;
        }
    }
}
