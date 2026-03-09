using Course.Catalog.API.Features.Courses.Dtos;
using Course.Catalog.API.Features.Courses.GetAll;
using Course.Catalog.API.Repositories;
using System.Net;

namespace Course.Catalog.API.Features.Courses.GetById
{
    public record GetCourseByIdQuery(Guid Id) : IRequestByServiceResult<CourseDto>;

    public class GetCourseByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCourseByIdQuery, ServiceResult<CourseDto>>
    {
        public async Task<ServiceResult<CourseDto>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
            if (course is null)
            {
                return ServiceResult<CourseDto>.Error("Course not found", HttpStatusCode.NotFound);
            }
            var category = await context.Categories.FindAsync(course.CategoryId, cancellationToken);
            course.Category = category!;
            var mappedCourse = mapper.Map<CourseDto>(course);
            return ServiceResult<CourseDto>.Success(mappedCourse);
        }
    }
    public static class GetCourseByIdEndpoint
    {
        public static RouteGroupBuilder GetCourseByIdGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator , Guid id) =>
                (await mediator.Send(new GetCourseByIdQuery(id))).ToGenericResult()).
                  WithName("GetByIdCourse")
                .MapToApiVersion(1, 0);


            return group;
        }
    }
}
