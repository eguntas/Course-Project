using AutoMapper;

namespace Course.Basket.API.Feature.Basket
{
    public class BasketMapping:Profile
    {
        public BasketMapping()
        {
            CreateMap<Data.Basket, Dtos.BasketDto>().ReverseMap();
            CreateMap<Data.BasketItem, Dtos.BasketItemDto>().ReverseMap();
        }
    }
}
