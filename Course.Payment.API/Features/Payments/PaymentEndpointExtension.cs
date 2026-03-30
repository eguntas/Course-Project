using Asp.Versioning.Builder;
using Course.Payment.API.Features.Payments.Create;
using Course.Payment.API.Features.Payments.GetAllPaymentByUserId;
using Course.Payment.API.Features.Payments.GetStatus;

namespace Course.Payment.API.Features.Payments
{
    public static class PaymentEndpointExtension
    {
        public static void AddPaymentEndpointExtension(this WebApplication app , ApiVersionSet apiVersion)
        {
            var group = app.MapGroup("/api/v{version:apiVersion}/payments").WithTags("Payments").WithApiVersionSet(apiVersion).
               CreatePaymentGroupItemEndpoint()
               .GetAllPaymentByUserIdGroupItemEndpoint()
               .GetPaymentStatusGroupItemEndpoint()
               .RequireAuthorization();
        }
    }
}
