using Course.Basket.API.Feature.Basket;
using Course.Bus.Events;
using MassTransit;

namespace Course.Basket.API.Consumers
{
    public class OrderCreatedEventConsumer(IServiceProvider serviceProvider) : IConsumer<OrderCreatedEvent>
    {
        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            using var scope = serviceProvider.CreateScope();
            var basketService = scope.ServiceProvider.GetRequiredService<BasketService>();
            await basketService.DeleteBasket(context.Message.UserId);
        }
    }
}
