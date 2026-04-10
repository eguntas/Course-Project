
using Course.Catalog.API.Features.Categories.Dtos;
using Course.Catalog.API.Repositories;

namespace Course.Catalog.API.Features.Categories.GetAll
{
    public class GetAllCategoryQuery:IRequestByServiceResult<List<CategoryDto>>;

    public class GetAllCategoryQueryHandler(AppDbContext context , IMapper mapper):IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync(cancellationToken);
            var mappedCategories = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(mappedCategories);
        }
    }


    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult()).
                 WithName("GetAllCategory").
                MapToApiVersion(1, 0).RequireAuthorization("ClientCredential");
                

            return group;
        }
    }
}
