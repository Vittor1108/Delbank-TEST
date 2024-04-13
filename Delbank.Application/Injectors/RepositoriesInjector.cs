using Delbank.Domain.Entities.SQL;
using Delbank.Domain.Interfaces.Repositories.NoSQL;
using Delbank.Domain.Interfaces.Repositories.SQL;
using Delbank.Domain.Interfaces.Services.SQL;
using Delbank.Infra.Data.NoSQL.Repository;
using Delbank.Infra.Data.SQL.Repository;
using Delbank.Service.Services.SQL;
using Microsoft.Identity.Client;

namespace Delbank.Application.Injectors
{
    public static class _RepositoriesInjector
    {
        public static IServiceCollection RepositoriesInjectr(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepositorySQL<DirectorEntitySQL>, BaseRepositorySQL<DirectorEntitySQL>>();

            services.AddScoped<IBaseRepositorySQL<DvdEntitySQL>, BaseRepositorySQL<DvdEntitySQL>>();
            services.AddScoped<IDvdRepositorySQL, DvdRepositorySQL>();


            services.AddSingleton<IDvdNoSQLRepository, DvdNoSqlRepository>();

            return services;
        }
    }
}
