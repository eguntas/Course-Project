using Course.Catalog.API.Repositories;

namespace Course.Catalog.API.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await context.Courses.FindAsync(request.id, cancellationToken);
            if (course is null)
                return ServiceResult.ErrorNoFound();

            course.Name = request.Name;
            course.Description = request.Description;
            course.ImageUrl = request.ImageUrl;
            course.Price = request.Price;
            course.CategoryId = request.CategoryId;

            context.Courses.Update(course);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
