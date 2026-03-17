using AutoMapper;
using Course.Order.Application.Contracts.Repositories;
using Course.Order.Application.Features.Orders.CreateOrder;
using Course.Shared;
using Course.Shared.Services;
using MediatR;

namespace Course.Order.Application.Features.Orders.GetOrder
{
    public class GetOrderQueryHandler(IIdentityService identityService , IOrderRepository orderRepository,IMapper mapper) : IRequestHandler<GetOrderQuery, ServiceResult<List<GetOrderQueryResponse>>>
    {
        public async Task<ServiceResult<List<GetOrderQueryResponse>>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await orderRepository.GetOrderByBuyerId(identityService.GetUserId);
            var response = orders.Select(x => new GetOrderQueryResponse(x.Created, x.TotalPrice, mapper.Map<List<OrderItemDto>>(x.OrderItems))).ToList();

            return ServiceResult<List<GetOrderQueryResponse>>.Success(response);    

        }
    }
}
