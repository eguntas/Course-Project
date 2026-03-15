namespace Course.Order.Domain.Entities
{
    public class OrderItem: BaseEntity<int>
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public decimal UnitPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public void SetItem(Guid ProductId , string ProductName , decimal UnitPrice)
        {
            if(string.IsNullOrEmpty(ProductName))
                throw new ArgumentNullException(nameof(ProductId), "ProductName cannot be null or empty.");

            if(UnitPrice <= 0)
                throw new ArgumentNullException(nameof(UnitPrice) , "UnitPrice must be greater than zero.");

            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.UnitPrice = UnitPrice;
        }

        public void UpdatePrice(decimal newPrice) {             
            if(newPrice <= 0)
                throw new ArgumentException("New price must be greater than zero.");
            this.UnitPrice = newPrice;
        }

        public void ApplyDiscount(float discountPercentage) {
            if(discountPercentage < 0 || discountPercentage > 100)
                throw new ArgumentException("Discount percentage must be between 0 and 100.");
            this.UnitPrice -= (this.UnitPrice * (decimal)discountPercentage / 100);
        }

        public bool IsSameItem(OrderItem orderItem)
        {
            return this.ProductId == orderItem.ProductId;
        }
    }
}
