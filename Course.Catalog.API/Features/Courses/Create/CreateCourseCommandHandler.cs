
using Course.Catalog.API.Repositories;
using System.Net;

namespace Course.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context , IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);
            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found", HttpStatusCode.NotFound);
            }
            var hasCourse = await context.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course with the same name already exists", HttpStatusCode.BadRequest);
            }

            var course = mapper.Map<Course>(request);
            course.Created = DateTime.UtcNow;
            course.Id = NewId.NextSequentialGuid();
            course.Feature = new Feature
            {
                Duration = 10,
                Rating = 1,
                EducatorFullName = "test name"
            };
            context.Courses.Add(course);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.Created(course.Id, $"/api/courses/{course.Id}");
        }
    }
}
