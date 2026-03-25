using Course.Order.Application.Features.Orders.CreateOrder;
using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Order.API.Endpoints.Orders
{
    public static class CreateOrderEndpoint
    {
        public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody]CreateOrderCommand command, [FromServices]IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult()).
                WithName("CreateOrder")
                .Produces<Guid>(StatusCodes.Status201Created)
                .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
                .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

            return group;
        }
    }
}
