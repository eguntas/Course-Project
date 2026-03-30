using Course.Order.Application.Contracts.Refit.PaymentService;
using Course.Shared.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Order.Application.Contracts.Refit
{
    public static class RefitConfiguration
    {
        public static IServiceCollection AddRefitConfigurationExt(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<AuthenticationHttpClientHandler>();
            services.AddScoped<ClientAuthenticationHttpClientHandler>();

            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            services.AddOptions<ClientSecretOption>().BindConfiguration(nameof(ClientSecretOption)).ValidateDataAnnotations().ValidateOnStart();

            services.AddSingleton<ClientSecretOption>(sp => sp.GetRequiredService<IOptions<ClientSecretOption>>().Value);

            services.AddRefitClient<IPaymentService>().ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri(configuration.GetSection(nameof(AddressUrlOption)).Get<AddressUrlOption>()!.PaymentUrl);
            }).AddHttpMessageHandler<AuthenticationHttpClientHandler>().AddHttpMessageHandler<ClientAuthenticationHttpClientHandler>();
            
            return services;
        }
    }
}
