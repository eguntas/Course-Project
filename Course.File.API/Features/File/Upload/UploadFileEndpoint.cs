using Course.Shared.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Course.File.API.Features.File.Upload
{
    public static class UploadFileEndpoint
    {
        public static RouteGroupBuilder UploadFileGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file, IMediator mediator) =>
                (await mediator.Send(new UploadFileCommand(file))).ToGenericResult()).
                WithName("Upload").
                MapToApiVersion(1, 0).
                Produces<Guid>(StatusCodes.Status201Created).
                Produces<ProblemDetails>(StatusCodes.Status400BadRequest).
                Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
                .DisableAntiforgery();

            return group;
        }
    }
}
