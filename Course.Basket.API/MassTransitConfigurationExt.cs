using Course.Basket.API.Consumers;
using Course.Bus;
using Course.Bus.Commands;
using Course.Bus.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Basket.API
{
    public static class MassTransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitBasketExt(this IServiceCollection services , IConfiguration configuration)
        {
            var busOption = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<OrderCreatedEventConsumer>();

                configure.UsingRabbitMq((ctx , cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOption.Address}:{busOption.Port}") , host =>
                    {
                        host.Username(busOption.UserName);
                        host.Password(busOption.Password);
                    });

                    cfg.ReceiveEndpoint("basket-microservice.order-created.queue", e =>
                    {
                        e.ConfigureConsumer<OrderCreatedEventConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
