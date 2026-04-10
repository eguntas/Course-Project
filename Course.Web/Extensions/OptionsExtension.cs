using Course.Web.Options;
using Microsoft.Extensions.Options;

namespace Course.Web.Extensions
{
    public static class OptionsExtension
    {
        public static IServiceCollection AddOptionExt(this IServiceCollection services) 
        {
            services.AddOptions<IdentityOption>().BindConfiguration(nameof(IdentityOption)).ValidateDataAnnotations().ValidateOnStart();
            
            services.AddSingleton<IdentityOption>(sp => sp.GetRequiredService<IOptions<IdentityOption>>().Value);

            services.AddOptions<GatewayOption>().BindConfiguration(nameof(GatewayOption)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<GatewayOption>(sp => sp.GetRequiredService<IOptions<GatewayOption>>().Value);

            services.AddOptions<MicroserviceOption>().BindConfiguration(nameof(MicroserviceOption)).ValidateDataAnnotations().ValidateOnStart();
            services.AddSingleton<MicroserviceOption>(sp => sp.GetRequiredService<IOptions<MicroserviceOption>>().Value);
            return services;
        }
    }
}
