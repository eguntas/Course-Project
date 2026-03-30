namespace Course.Order.Application.Contracts.Refit.PaymentService
{
    public record CreatePaymentResponse(Guid? PaymentId ,bool Status , string? errorMessage);
    
}
