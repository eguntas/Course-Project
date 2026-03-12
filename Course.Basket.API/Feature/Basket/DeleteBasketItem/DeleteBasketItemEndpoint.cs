using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Basket.API.Feature.Basket.DeleteBasketItem
{
    public static class DeleteBasketItemEndpoint
    {
        public static RouteGroupBuilder DeleteBasketItemGroupItemEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapDelete("/item/{id:guid}", async ([FromBody]Guid id, [FromServices]IMediator mediator) =>
                (await mediator.Send(new DeleteBasketItemCommand(id))).ToGenericResult())
                .WithName("DeleteBasketItem")
                .MapToApiVersion(1, 0);
                //.AddEndpointFilter<ValidationFilter<DeleteBasketItemCommandValidator>>();

            return builder;
        }
    }
}
