using Course.Catalog.API.Features.Courses.Dtos;
using Course.Catalog.API.Features.Courses.GetById;
using Course.Catalog.API.Repositories;
using System.Net;

namespace Course.Catalog.API.Features.Courses.GetAllByUserId
{

    public record GetCourseByUserIdQuery(Guid Id) : IRequestByServiceResult<List<CourseDto>>;

    public class GetCourseByUserIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByUserIdQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetCourseByUserIdQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.Where(x=>x.UserId==request.Id).ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);
            foreach (var course in courses)
            {
                course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
            }


            var mapping = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.Success(mapping);
        }
    }
    public static class GetCourseByUserIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByUserIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/user/{userId:guid}", async (IMediator mediator, Guid userId) =>
                (await mediator.Send(new GetCourseByUserIdQuery(userId))).ToGenericResult()).
                WithName("GetByUserIdCourses").
                MapToApiVersion(1,0);


            return group;
        }
    }
}
