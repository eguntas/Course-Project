using Course.Order.Application.Contracts.Refit.PaymentService;
using Course.Order.Application.Contracts.Repositories;
using Course.Order.Application.Contracts.UnitOfWorks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.BackgroundServices
{
    public class CheckPaymentStatusOrderBackgroundService(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = serviceProvider.CreateScope();
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            while (!stoppingToken.IsCancellationRequested)
            {
                var waitingOrder = orderRepository.Where(x => x.Status == Domain.Entities.OrderStatus.WaitingForPayment).ToList();


                foreach (var order in waitingOrder)
                {
                    var paymentStatus = await paymentService.GetPaymentStatusAsync(order.Code);

                    if (paymentStatus.isPaid)
                    {
                        await orderRepository.SetStatus(order.Code, paymentStatus.PaymentId!.Value, Domain.Entities.OrderStatus.Paid);
                        await unitOfWork.CommitAsync(stoppingToken);
                    }
                }
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
           
        }
    }
}
