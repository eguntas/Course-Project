using Course.Shared.Extensions;
using Course.Shared.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.Payment.API.Features.Payments.GetAllPaymentByUserId
{
    public static class GetAllPaymentByUserIdCommandEndpoint
    {
        public static RouteGroupBuilder GetAllPaymentByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("", async (IMediator mediator) =>
               (await mediator.Send(new GetAllPaymentByUserIdQuery())).ToGenericResult()).
               WithName("get-all-payments-by-userId").
               MapToApiVersion(1, 0).
               Produces(StatusCodes.Status200OK).
               Produces<ProblemDetails>(StatusCodes.Status400BadRequest).
               Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
