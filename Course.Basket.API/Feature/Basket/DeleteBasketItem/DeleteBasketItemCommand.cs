using Course.Shared;

namespace Course.Basket.API.Feature.Basket.DeleteBasketItem
{
    public record DeleteBasketItemCommand(Guid Id):IRequestByServiceResult;
}
