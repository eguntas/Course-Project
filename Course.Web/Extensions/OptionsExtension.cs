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

            return services;
        }
    }
}
