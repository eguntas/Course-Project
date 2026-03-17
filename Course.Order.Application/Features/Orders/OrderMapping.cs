using AutoMapper;
using Course.Order.Application.Features.Orders.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Features.Orders
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<Domain.Entities.OrderItem, OrderItemDto>().ReverseMap();
        }
    }
}
