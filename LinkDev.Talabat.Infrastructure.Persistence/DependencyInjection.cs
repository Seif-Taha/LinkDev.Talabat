using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
                optionsBulider
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("StoreContext"));
            } /*,contextLifetime: ServiceLifetime.Scoped , optionsLifetime: ServiceLifetime.Scoped*/ );

            services.AddScoped<IStoreContextIntializer,StoreContextIntializer>();
            
            services.AddScoped(typeof(IUnitOfWork) , typeof(UnitOfWork.UnitOfWork));
            services.AddScoped(typeof(IStoreContextIntializer), typeof(StoreContextIntializer));

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));
            
            return services;

        }
    }
}
