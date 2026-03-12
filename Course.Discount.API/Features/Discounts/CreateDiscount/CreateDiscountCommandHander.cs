
using Course.Discount.API.Repositories;
using Course.Shared.Services;
using System.Net;

namespace Course.Discount.API.Features.Discounts.CreateDiscount
{
    public class CreateDiscountCommandHander(AppDbContext dbContext , IIdentityService identityService) : 
        IRequestHandler<CreateDiscountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
        {
            var hasCodeForUser = await dbContext.Discounts.AnyAsync(x => x.Code == request.Code && x.UserId == request.UserId, cancellationToken);
            
            if(hasCodeForUser)
                return ServiceResult.Error("Code already exists for user", HttpStatusCode.BadRequest);


            var discount = new Discount
            {
                Id = NewId.NextSequentialGuid(),
                Code = request.Code,
                Rate = request.Rate,
                UserId = request.UserId,
                Expired = request.Expired,
                Created = DateTime.UtcNow
            };
            await dbContext.Discounts.AddAsync(discount , cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
