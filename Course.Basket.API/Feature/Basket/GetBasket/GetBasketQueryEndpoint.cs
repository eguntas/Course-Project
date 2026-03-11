using Course.Shared.Extensions;
using MediatR;

namespace Course.Basket.API.Feature.Basket.GetBasket
{
    public static class GetBasketQueryEndpoint
    {
        public static RouteGroupBuilder GetBasketGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user", async (IMediator mediator) =>
                (await mediator.Send(new GetBasketQuery())).ToGenericResult()).
                 WithName("GetBasket").
                MapToApiVersion(1, 0);


            return group;
        }
    }
}
