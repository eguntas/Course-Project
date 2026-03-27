using Course.Bus.Events;
using Course.Order.Application.Contracts.Repositories;
using Course.Order.Application.Contracts.UnitOfWorks;
using Course.Order.Domain.Entities;
using Course.Shared;
using Course.Shared.Services;
using MassTransit;
using MediatR;
using System.Reflection.Metadata;

namespace Course.Order.Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandHandler(IOrderRepository orderRepository , IGenericRepository<int,Address> addressRepository , IIdentityService identityService , IUnitOfWork unitOfWork , IPublishEndpoint publishEndpoint) 
        : IRequestHandler<CreateOrderCommand, ServiceResult>
    {
      
        public async Task<ServiceResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.Items.Any())
            {
                return ServiceResult.Error("Invalid Order", "Order must contain at least one item.", System.Net.HttpStatusCode.BadRequest);
            }

            var newAddress = new Address{
                Province = request.Address.Province,
                District = request.Address.District,
                Street = request.Address.Street,
                ZipCode = request.Address.ZipCode,
                Line = request.Address.Line
            };


            var order = Domain.Entities.Order.CreateUnPaidOrder(identityService.GetUserId ,request.DiscountRate , newAddress.Id);

            foreach(var orderItem in request.Items)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.UnitPrice);
            }

            order.Address = newAddress;

            orderRepository.Add(order);
            await unitOfWork.CommitAsync(cancellationToken);

            var paymentId = Guid.Empty;
            order.SetPaid(paymentId);

            orderRepository.Update(order);
            await unitOfWork.CommitAsync(cancellationToken);

            await publishEndpoint.Publish(new OrderCreatedEvent(order.Id , identityService.GetUserId) , cancellationToken);

            return ServiceResult.SuccessAsNoContent(); 

        }
    }
}
