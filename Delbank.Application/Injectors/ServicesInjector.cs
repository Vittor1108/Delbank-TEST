using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Interfaces.Services.SQL;
using Delbank.Service.Services.SQL;

namespace Delbank.Application.Injectors
{
    public static class _ServicesInjector
    {
        public static IServiceCollection ServicesInjecetor(this IServiceCollection services)
        {
            services.AddScoped<IBaseServiceSQL<DirectorEntitySQL>, BaseServiceSQL<DirectorEntitySQL>>();

            services.AddScoped<IBaseServiceSQL<DvdEntitySQL>, BaseServiceSQL<DvdEntitySQL>>();
            services.AddScoped<IDvdServiceSQL, DvdServiceSQL>();

            return services;
        }
    }
}
