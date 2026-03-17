using Course.Order.Application.Features.Orders.CreateOrder;
using Course.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Features.Orders.GetOrder
{
    public record GetOrderQueryResponse(DateTime Created , decimal TotalPrice , List<OrderItemDto> Items);
}
