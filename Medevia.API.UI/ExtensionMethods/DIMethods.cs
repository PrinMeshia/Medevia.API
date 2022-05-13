using Medevia.Core.Domain;
using MediatR;

namespace Medevia.API.UI.ExtensionMethods
{
    public static class DIMethods
    {
        #region Public methods
        /// <summary>
        /// prepare customs Dependency Injections
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection  AddInjections(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Program));
            return services;
        }
        #endregion
    }
}
