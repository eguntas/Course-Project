using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Features.Orders.GetOrder
{
    public record GetOrderQuery : IRequestByServiceResult<List<GetOrderQueryResponse>>;
}
