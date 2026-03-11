using Course.Basket.API.Dtos;
using Course.Shared;

namespace Course.Basket.API.Feature.Basket.GetBasket
{
    public record GetBasketQuery:IRequestByServiceResult<BasketDto>;
    
}
