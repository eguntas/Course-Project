using MassTransit;
using System.Text;

namespace Course.Order.Domain.Entities
{
    public class Order:BaseEntity<Guid>
    {

        public string Code { get; set; } = null!;
        public DateTime Created { get; set; }
        public Guid BuyerId { get; set; }
        public OrderStatus Status { get; set; }
        public int AddressId { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? PaymentId { get; set; }
        public float? DiscountRate { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
        public Address Address { get; set; } = null!;

        public static string GenerateOrderCode()
        {
            var random = new Random();
            var orderCode = new StringBuilder(10);

            for (int i = 0; i < 10; i++)
            {
                orderCode.Append(random.Next(0, 10));
            }

            return orderCode.ToString();
        }

        public static Order CreateUnPaidOrder(Guid BuyerId , float? discountRate , int addressId) 
        { 
            return new Order
            {
                Id = NewId.NextGuid(),
                Code = GenerateOrderCode(),
                Created = DateTime.Now,
                BuyerId = BuyerId,
                Status = OrderStatus.WaitingForPayment,
                AddressId = addressId,
                DiscountRate = discountRate,
                TotalPrice = 0
            };
        }

        public void AddOrderItem(Guid productId , string productName , decimal unitPrice)
        {
            var orderItem = new OrderItem();
            orderItem.SetItem(productId, productName, unitPrice);
            OrderItems.Add(orderItem);
            CalculateTotalPrice();
        }
        

        public void ApplyDiscount(float discountRate)
        {
            if (discountRate < 0 || discountRate > 100)
                throw new ArgumentException("Discount rate must be between 0 and 100.");
            DiscountRate = discountRate;
            CalculateTotalPrice();
        }

        public void SetPaid(Guid paymentId)
        {
            PaymentId = paymentId;
            this.Status = OrderStatus.Paid;
        }
        private void CalculateTotalPrice()
        {
            TotalPrice = OrderItems.Sum(item => item.UnitPrice);
            if (DiscountRate.HasValue)
            {
                TotalPrice -= TotalPrice * (decimal)DiscountRate.Value / 100;
            }
        }
    }

    
}
