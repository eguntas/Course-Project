using Course.Payment.API.Repositories;
using Course.Shared;
using Course.Shared.Services;
using MediatR;

namespace Course.Payment.API.Features.Payments.Create
{
    public class CreatePaymentCommandHandler(AppDbContext dbContext , IIdentityService identityService):IRequestHandler<CreatePaymentCommand , ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            var (isSuccess , errorMessage) = await ExternalPaymentProcessAsync(request.CardNumber, request.CardHolderName, request.CardExpirationDate, request.CardSecurityNumber, request.Amount);

            if (!isSuccess)
            {
                return ServiceResult<Guid>.Error("Payment processing failed", errorMessage, System.Net.HttpStatusCode.BadRequest);
            }

            var newPayment = new Repositories.Payment(identityService.GetUserId, request.OrderCode, request.Amount);
            newPayment.SetPaymentStatus(PaymentStatus.Success);

            dbContext.Payments.Add(newPayment);
            await dbContext.SaveChangesAsync(cancellationToken);
            return ServiceResult<Guid>.Success(newPayment.Id);

        }

        private async Task<(bool isSuccess, string? errorMessage)> ExternalPaymentProcessAsync(string cardNumber , string cardHolderName, string cardExpirationDate , string cardSecurityNumber , decimal amount)
        {
            // Simulate an external payment process (e.g., calling a payment gateway)
            await Task.Delay(1000); // Simulate some delay
            return (true,null); // Assume the payment is successful
        }
    }
}
