using Course.Order.Application.Features.Orders.GetOrder;
using Course.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Order.API.Endpoints.Orders
{
    public static class GetOrderEndpoint
    {
        public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetOrderQuery())).ToGenericResult()).
                WithName("GetOrders")
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .MapToApiVersion(1, 0);

            return group;
        }
    }
}
