using Course.Catalog.API.Features.Categories.Create;

namespace Course.Catalog.API.Features.Courses.Create
{
    public static class CreateCourseCommandEndpoint
    {
        public static RouteGroupBuilder CreateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (CreateCourseCommand command, IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult()).
                WithName("CreateCourse")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<CreateCourseCommand>>();

            return group;
        }
    }
}
