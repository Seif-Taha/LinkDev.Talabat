using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services , IConfiguration configuration) 
        {

            services.AddDbContext<StoreContext>((optionsBulider) =>
            {
                optionsBulider.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            } /*,contextLifetime: ServiceLifetime.Scoped , optionsLifetime: ServiceLifetime.Scoped*/ );

            services.AddScoped<IStoreContextIntializer,StoreContextIntializer>();
            services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreContextIntializer));

            return services;

        }
    }
}
