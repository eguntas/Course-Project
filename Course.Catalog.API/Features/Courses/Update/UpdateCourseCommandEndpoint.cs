using Course.Catalog.API.Features.Courses.GetById;

namespace Course.Catalog.API.Features.Courses.Update
{
    public static class UpdateCourseCommandEndpoint
    {
        public static RouteGroupBuilder UpdateCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapPut("/", async (UpdateCourseCommand command , IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
                .AddEndpointFilter<ValidationFilter<UpdateCourseCommandValidator>>();


            return group;
        }
    }
}
