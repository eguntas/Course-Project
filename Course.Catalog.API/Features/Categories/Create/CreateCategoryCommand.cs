using Course.Shared;
using MediatR;

namespace Course.Catalog.API.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name):IRequest<ServiceResult<CreateCategoryResponse>>;
}
