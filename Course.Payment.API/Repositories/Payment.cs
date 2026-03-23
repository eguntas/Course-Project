using MassTransit;

namespace Course.Payment.API.Repositories
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string OrderCode { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment(Guid userId, string orderCode, decimal amount)
        {
            Create(userId, orderCode, amount);
        }

        public void Create(Guid userId, string orderCode, decimal amount)
        {
            Id = NewId.NextSequentialGuid();
            UserId = userId;
            OrderCode = orderCode;
            Amount = amount;
            Created = DateTime.UtcNow;
            Status = PaymentStatus.Pending;
        }

        public void SetPaymentStatus(PaymentStatus status)
        {
            Status = status;
        }

    }

    public enum PaymentStatus
    {
        Success = 1,
        Failed ,
        Pending
    }
}
