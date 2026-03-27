using Course.Bus.Commands;
using Course.File.API.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Course.Bus
{
    public static class MassTransitConfigurationExt
    {
        public static IServiceCollection AddMasstransitFileExt(this IServiceCollection services , IConfiguration configuration)
        {
            var busOption = configuration.GetSection(nameof(BusOption)).Get<BusOption>()!;

            services.AddMassTransit(configure =>
            {
                configure.AddConsumer<UploadCoursePictureCommandConsumer>();

                configure.UsingRabbitMq((ctx , cfg) =>
                {
                    cfg.Host(new Uri($"rabbitmq://{busOption.Address}:{busOption.Port}") , host =>
                    {
                        host.Username(busOption.UserName);
                        host.Password(busOption.Password);
                    });

                    cfg.ReceiveEndpoint("file-microservice.upload-course-picture-command.queue", e =>
                    {
                        e.ConfigureConsumer<UploadCoursePictureCommandConsumer>(ctx);
                    });
                });
            });

            return services;
        }
    }
}
