using AutoMapper;
using Course.Catalog.API.Features.Categories.Dtos;
using Course.Catalog.API.Repositories;
using Course.Shared;
using Course.Shared.Extensions;
using MediatR;
using System.Net;

namespace Course.Catalog.API.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;

    public class GetCategoryByIdHandler(AppDbContext context , IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await context.Categories.FindAsync(request.Id , cancellationToken);

            if (category is null)
            {
                return ServiceResult<CategoryDto>.Error("Category Not Found", HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(categoryDto);

        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator , Guid id) =>
                (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult());


            return group;
        }
    }
}
