using Course.Payment.API.Repositories;

namespace Course.Payment.API.Features.Payments.GetAllPaymentByUserId
{
    public record GetAllPaymentByUserIdQueryResponse(Guid Id , string OrderCode , string Amount , DateTime Created, PaymentStatus Status);
}
