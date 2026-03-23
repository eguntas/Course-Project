using Course.Payment.API.Repositories;
using Course.Shared;
using Course.Shared.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Course.Payment.API.Features.Payments.GetAllPaymentByUserId
{
    public class GetAllPaymentByUserIdQueryHandler(AppDbContext context , IIdentityService identityService) : IRequestHandler<GetAllPaymentByUserIdQuery, ServiceResult<List<GetAllPaymentByUserIdQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetAllPaymentByUserIdQueryResponse>>> Handle(GetAllPaymentByUserIdQuery request, CancellationToken cancellationToken)
        {
           var userId = identityService.GetUserId;
              var payments = await context.Payments.Where(p => p.UserId == userId).Select(p => new GetAllPaymentByUserIdQueryResponse
              (
                  p.Id,
                  p.OrderCode,
                  p.Amount.ToString("C"),
                  p.Created,
                  p.Status
              )).ToListAsync();

            return ServiceResult<List<GetAllPaymentByUserIdQueryResponse>>.Success(payments);
        }
    }
}
