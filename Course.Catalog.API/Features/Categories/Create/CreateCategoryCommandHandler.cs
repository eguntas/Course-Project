using Course.Catalog.API.Repositories;
using Course.Shared;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Course.Catalog.API.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existCategory = await context.Categories.AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (existCategory)
            {
                ServiceResult<CreateCategoryResponse>.Error($"Category name exist", $"CategoryName {request.Name} already exist", System.Net.HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };
            await context.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            return ServiceResult<CreateCategoryResponse>.Created(new CreateCategoryResponse(category.Id), "<empty>");
            
        }
    }
}
