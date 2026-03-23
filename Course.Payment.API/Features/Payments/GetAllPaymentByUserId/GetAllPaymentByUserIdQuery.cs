using Course.Shared;

namespace Course.Payment.API.Features.Payments.GetAllPaymentByUserId
{
    public record GetAllPaymentByUserIdQuery:IRequestByServiceResult<List<GetAllPaymentByUserIdQueryResponse>>;
 
}
