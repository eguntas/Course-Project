using System.Text.Json.Serialization;

namespace Course.Basket.API.Data
{
    public class Basket
    {
        public Guid UserId { get; set; }
        public List<BasketItem> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        [JsonIgnore]
        public decimal TotalPrice => Items.Sum(x => x.Price);
        [JsonIgnore]

        public bool IsApplyDiscount => DiscountRate is > 0 && string.IsNullOrEmpty(Coupon);

        public Basket()
        {
        }
        public Basket(Guid userId , List<BasketItem> items)
        {
            UserId = userId;
            Items = items;
        }

        [JsonIgnore]

        public decimal? TotalPriceByApplyDiscountRate => 
            !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);

        public void ApplyNewDiscount(float discountRate, string coupon)
        {
            DiscountRate = discountRate;
            Coupon = coupon;
            foreach (var item in Items)
            {
                item.PriceByApplyDiscountRate = item.Price * (decimal)(1-discountRate);
            }
        }

        public void ApplyAvaibleDiscount()
        {
            if (!IsApplyDiscount)
                return;


            foreach (var item in Items)
            {
                item.PriceByApplyDiscountRate = item.Price * (decimal)(1 - DiscountRate!);
            }
        }

        public void ClearDiscount()
        {
            DiscountRate = null;
            Coupon = null;
            foreach(var basket in Items)
            {
                basket.PriceByApplyDiscountRate = null;
            }
        }



    }
}
