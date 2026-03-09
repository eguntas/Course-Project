using Course.Catalog.API.Features.Categories.Create;
using Course.Catalog.API.Features.Categories.GetAll;
using Course.Catalog.API.Features.Courses.Dtos;
using Course.Catalog.API.Repositories;


namespace Course.Catalog.API.Features.Courses.GetAll
{
    public record GetAllCoursesQuery : IRequestByServiceResult<List<CourseDto>>;

    public class GetAllCoursesQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetAllCoursesQuery, ServiceResult<List<CourseDto>>>
    {
        public async Task<ServiceResult<List<CourseDto>>> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var courses = await context.Courses.ToListAsync(cancellationToken);
            var categories = await context.Categories.ToListAsync(cancellationToken);
            foreach (var course in courses)
            {
                course.Category = categories.FirstOrDefault(c => c.Id == course.CategoryId);
            }


            var mapping = mapper.Map<List<CourseDto>>(courses);
            return ServiceResult<List<CourseDto>>.Success(mapping);
        }
    }

    public static class GetAllCoursesEndpoint
    {   
        public static RouteGroupBuilder GetAllCourseGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllCoursesQuery())).ToGenericResult()).
                  WithName("GetAllCourse")
                .MapToApiVersion(1, 0);


            return group;
        }

    }
}
