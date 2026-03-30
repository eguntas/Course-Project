using Course.Payment.API.Features.Payments.GetAllPaymentByUserId;
using Course.Payment.API.Repositories;
using Course.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Course.Payment.API.Features.Payments.GetStatus
{
    public record GetPaymentStatusRequest(string orderCode):IRequestByServiceResult<GetPaymentStatusResponse>;
    public record GetPaymentStatusResponse(Guid? PaymentId , bool isPaid);
    public class GetPaymentStatusQueryHandler(AppDbContext dbContext) : IRequestHandler<GetPaymentStatusRequest, ServiceResult<GetPaymentStatusResponse>>
    {
        public async Task<ServiceResult<GetPaymentStatusResponse>> Handle(GetPaymentStatusRequest request, CancellationToken cancellationToken)
        {
            var payment = await dbContext.Payments.FirstOrDefaultAsync(x => x.OrderCode == request.orderCode , cancellationToken);

            if (payment == null)
            {
                return ServiceResult<GetPaymentStatusResponse>.Success(new GetPaymentStatusResponse(null , false));
            }

            return ServiceResult<GetPaymentStatusResponse>.Success(new GetPaymentStatusResponse(payment.Id , payment.Status == PaymentStatus.Success));
        }
    }
}
