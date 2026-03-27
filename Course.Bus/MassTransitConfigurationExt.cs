using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Bus
{
    public static class MassTransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitExt(this IServiceCollection services , IConfiguration configuration)
        {
            var busOption = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

            services.AddMassTransit(configure =>
            {
                configure.UsingRabbitMq((ctx , cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOption.Address}:{busOption.Port}") , host =>
                    {
                        host.Username(busOption.UserName);
                        host.Password(busOption.Password);
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });

            return services;
        }
    }
}
