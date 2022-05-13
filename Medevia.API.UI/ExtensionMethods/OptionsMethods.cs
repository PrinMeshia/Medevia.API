using Medevia.Core.Infrastructure.Configurations;

namespace Medevia.API.UI.ExtensionMethods
{
    /// <summary>
    /// Customs options for configs
    /// </summary>
    public static class OptionsMethods
    {
        /// <summary>
        /// Add customs options for configs
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SecurityOption>(configuration.GetSection("Jwt"));
            return services;
        }
    }
}
