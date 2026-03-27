using Course.Bus.Events;
using Course.Discount.API.Features.Discounts;
using Course.Discount.API.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Threading;


namespace Course.Discount.API.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var discount = new Features.Discounts.Discount
            {
                Id = NewId.NextSequentialGuid(),
                Code = CodeGenerator.Generate(10),
                Rate = 0.1f,
                UserId = context.Message.UserId,
                Expired = DateTime.Now.AddMonths(1),
                Created = DateTime.UtcNow
            };
            await appDbContext.Discounts.AddAsync(discount);
            await appDbContext.SaveChangesAsync();
        }
    }
}
