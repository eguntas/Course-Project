using Course.Catalog.API.Features.Categories.Create;
using Microsoft.AspNetCore.Mvc;

namespace Course.Catalog.API.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromForm]CreateCourseCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult()).
                WithName("CreateCourse")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>().DisableAntiforgery();

            return group;
        }
    }
}
