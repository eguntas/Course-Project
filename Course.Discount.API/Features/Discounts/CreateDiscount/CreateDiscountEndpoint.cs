using Microsoft.AspNetCore.Mvc;

namespace Course.Discount.API.Features.Discounts.CreateDiscount
{
    public static class CreateDiscountEndpoint
    {
        public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult()).
                WithName("CreateDiscount").
                MapToApiVersion(1, 0).
                Produces<Guid>(StatusCodes.Status201Created).
                Produces<ProblemDetails>(StatusCodes.Status400BadRequest).
                Produces<ProblemDetails>(StatusCodes.Status500InternalServerError).
                AddEndpointFilter<ValidationFilter<CreateDiscountCommandValidation>>();

            return group;
        }
    }
}
