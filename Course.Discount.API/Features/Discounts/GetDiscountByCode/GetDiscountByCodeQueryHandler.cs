using Course.Discount.API.Repositories;
using Course.Shared.Services;
using System.Net;

namespace Course.Discount.API.Features.Discounts.GetDiscountByCode
{
    public class GetDiscountByCodeQueryHandler(AppDbContext appDbContext , IIdentityService identityService) : IRequestHandler<GetDiscountByCodeQuery, ServiceResult<GetDiscountByCodeQueryResponse>>
    {
        public async Task<ServiceResult<GetDiscountByCodeQueryResponse>> Handle(GetDiscountByCodeQuery request, CancellationToken cancellationToken)
        {
            var hasDiscount = await appDbContext.Discounts.SingleOrDefaultAsync(x => x.Code == request.Code, cancellationToken);

            if (hasDiscount is null)
                return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount not found", HttpStatusCode.NotFound);

            if (hasDiscount.Expired < DateTime.Now)
                 return ServiceResult<GetDiscountByCodeQueryResponse>.Error("Discount is expired", HttpStatusCode.BadRequest);
            
           
            return ServiceResult<GetDiscountByCodeQueryResponse>.Success(new GetDiscountByCodeQueryResponse(hasDiscount.Code, hasDiscount.Rate));
             
        }
            
    }
}
