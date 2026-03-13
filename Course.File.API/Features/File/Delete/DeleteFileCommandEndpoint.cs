using Course.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.File.API.Features.File.Delete
{
    public static class DeleteFileCommandEndpoint
    {
        public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/", async ([FromBody]DeleteFileCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult()).
                WithName("Delete").
                MapToApiVersion(1, 0).
                Produces<Guid>(StatusCodes.Status201Created).
                Produces<ProblemDetails>(StatusCodes.Status400BadRequest).
                Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

            return group;
        }
    }
}
