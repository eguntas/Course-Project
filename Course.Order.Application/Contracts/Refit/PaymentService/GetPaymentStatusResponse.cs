using Course.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Contracts.Refit.PaymentService
{
    public record GetPaymentStatusResponse(Guid? PaymentId, bool isPaid);


}
