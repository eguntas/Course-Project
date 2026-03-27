using Course.Bus;
using Course.Bus.Commands;
using Course.Bus.Events;
using Course.Catalog.API.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Catalog.API
{
    public static class MassTransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitCatalogExt(this IServiceCollection services , IConfiguration configuration)
        {
            var busOption = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<CoursePictureUplodedEventConsumer>();

                configure.UsingRabbitMq((ctx , cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOption.Address}:{busOption.Port}") , host =>
                    {
                        host.Username(busOption.UserName);
                        host.Password(busOption.Password);
                    });

                    cfg.ReceiveEndpoint("catalog-microservice.course-picture-uploaded.queue", e =>
                    {
                        e.ConfigureConsumer<CoursePictureUplodedEventConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
