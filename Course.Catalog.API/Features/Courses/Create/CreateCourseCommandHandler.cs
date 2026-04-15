using Course.Bus.Commands;
using Course.Catalog.API.Repositories;
using Course.Shared.Services;
using System.Net;

namespace Course.Catalog.API.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context , IMapper mapper , IPublishEndpoint publishEndpoint , IIdentityService identityService) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
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
            course.UserId = identityService.GetUserId;
            course.Id = NewId.NextSequentialGuid();
            course.Feature = new Feature
            {
                Duration = 10,
                Rating = 1,
                EducatorFullName = "test name"
            };
            context.Courses.Add(course);
            await context.SaveChangesAsync(cancellationToken);

            if(request.Picture is not null)
            {
                using var memoryStream = new MemoryStream();
                
                await request.Picture.CopyToAsync(memoryStream , cancellationToken);
                var pictureAsByteArr = memoryStream.ToArray();
                
                var uploadCoursePictureCommand = 
                    new UploadCoursePictureCommand(course.Id, pictureAsByteArr , request.Picture.FileName);
                
                await publishEndpoint.Publish(uploadCoursePictureCommand , cancellationToken);

            }


            return ServiceResult<Guid>.Created(course.Id, $"/api/courses/{course.Id}");
        }
    }
}
