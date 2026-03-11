using AutoMapper;
using Course.Basket.API.Dtos;
using Course.Shared;
using MediatR;
using System.Net;
using System.Text.Json;

namespace Course.Basket.API.Feature.Basket.GetBasket
{
    public class GetBasketQueryHandler(IMapper mapper , BasketService basketService) : IRequestHandler<GetBasketQuery, ServiceResult<BasketDto>>
    {
        public async Task<ServiceResult<BasketDto>> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
         
            var basketAsString = await basketService.GetBasketCacheKeyAsync(cancellationToken);

            if (string.IsNullOrEmpty(basketAsString))
            {
                return ServiceResult<BasketDto>.Error("Basket not found", HttpStatusCode.NotFound);
            }
            var basket = JsonSerializer.Deserialize<Data.Basket>(basketAsString);
            var basketDto = mapper.Map<BasketDto>(basket);

            return ServiceResult<BasketDto>.Success(basketDto);
        }
    }
}
