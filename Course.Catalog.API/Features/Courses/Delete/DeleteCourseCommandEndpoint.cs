
using Course.Catalog.API.Features.Courses.Update;
using Course.Catalog.API.Repositories;

namespace Course.Catalog.API.Features.Courses.Delete
{
    public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;
    public class DeleteCourseHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.Id, cancellationToken);
            if(course is null)
                return ServiceResult.ErrorNoFound();

            context.Courses.Remove(course);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
    public static class DeleteCourseCommandEndpoint
    {
        public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapDelete("/{id:guid}", async (IMediator mediator , Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult()).
                WithName("DeleteCourse").
                MapToApiVersion(1,0);

            return group;
        }
    }
}
