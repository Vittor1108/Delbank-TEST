using Delbank.Commons;
using Delbank.Commons.Interfaces;
using Microsoft.Identity.Client;

namespace Delbank.Application.Injectors
{
    public static class _CommonsInjector
    {
        public static IServiceCollection CommonsInjector(this IServiceCollection services)
        {
            services.AddScoped<IResponseCommon, ResponseCommon>();
            return services;
        }
    }
}
