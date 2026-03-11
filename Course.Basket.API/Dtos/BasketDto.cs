using System.Text.Json.Serialization;

namespace Course.Basket.API.Dtos
{
    public record BasketDto
    {
        [JsonIgnore] public Guid UserId { get; init; }
        public List<BasketItemDto> Items { get; set; } = new();
        public float? DiscountRate { get; set; }
        public string? Coupon { get; set; }

        public BasketDto(Guid userId , List<BasketItemDto> items)
        {
            UserId = userId;
            Items = items;
        }

        public BasketDto()
        {
        }

       
        public decimal TotalPrice => Items.Sum(x => x.Price);

        public decimal? TotalPriceByApplyDiscountRate =>
            !IsApplyDiscount ? null : Items.Sum(x => x.PriceByApplyDiscountRate);


        [JsonIgnore] public bool IsApplyDiscount => DiscountRate is > 0 && string.IsNullOrEmpty(Coupon);

 

    };

   
}
