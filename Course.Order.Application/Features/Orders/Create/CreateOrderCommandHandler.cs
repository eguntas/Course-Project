using Course.Order.Application.Contracts.Repositories;
using Course.Order.Domain.Entities;
using Course.Shared;
using Course.Shared.Services;
using MediatR;

namespace Course.Order.Application.Features.Orders.Create
{
    public class CreateOrderCommandHandler(IGenericRepository<Guid , Domain.Entities.Order> orderRepository , IGenericRepository<int,Address> addressRepository , IIdentityService identityService) : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
      
        public Task<ServiceResult> IRequestHandler<CreateOrderCommand, ServiceResult>.Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Items.Any())
            {
                return Task.FromResult(ServiceResult.Error("Invalid Order", "Order must contain at least one item.", System.Net.HttpStatusCode.BadRequest));
            }

            var newAddress = new Address{
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };

            addressRepository.Add(newAddress);

            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId ,request.DiscountRate , newAddress.Id);

            foreach(var orderItem in request.Items)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
            }
            orderRepository.Add(order);
            var paymentId = Guid.Empty;
            order.SetPaid(paymentId);

            orderRepository.Update(order);

            return Task.FromResult(ServiceResult.SuccessAsNoContent()); 

        }
    }
}
