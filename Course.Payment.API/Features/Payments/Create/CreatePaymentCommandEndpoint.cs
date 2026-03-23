using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Payment.API.Features.Payments.Create
{
    public static class GetAllPaymentByUserIdCommandEndpoint
    {
        public static RouteGroupBuilder CreatePaymentGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreatePaymentCommand command, IMediator mediator) =>
               (await mediator.Send(command)).ToGenericResult()).
               WithName("CreatePayment").
               MapToApiVersion(1, 0).
               Produces(StatusCodes.Status204NoContent).
               Produces<ProblemDetails>(StatusCodes.Status400BadRequest).
               Produces<ProblemDetails>(StatusCodes.Status500InternalServerError).
               AddEndpointFilter<ValidationFilter<CreatePaymentCommand>>();

            return group;
        }
    }
}
