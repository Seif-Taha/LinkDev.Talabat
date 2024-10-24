using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence.DbInitializer;
using LinkDev.Talabat.Core.Domain.Entities.Identity;
using LinkDev.Talabat.Infrastructure.Persistence._Data.Interceptors;
using LinkDev.Talabat.Infrastructure.Persistence._Identity;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services , IConfiguration configuration) 
        {

            #region Store Context

            services.AddDbContext<StoreContext>((optionsBulider) =>
                {
                    optionsBulider
                    .UseLazyLoadingProxies()
                    .UseSqlServer(configuration.GetConnectionString("StoreContext"));
                } /*,contextLifetime: ServiceLifetime.Scoped , optionsLifetime: ServiceLifetime.Scoped*/ );

            services.AddScoped(typeof(IStoreDbIntializer), typeof(StoreDbInitializer));

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(BaseAuditableEntityInterceptor));

            #endregion

            #region Identity Context

            services.AddDbContext<StoreIdentityContext>((optionsBulider) =>
            {
                optionsBulider
                .UseLazyLoadingProxies()
                .UseSqlServer(configuration.GetConnectionString("IdentityContext"));
            } /*,contextLifetime: ServiceLifetime.Scoped , optionsLifetime: ServiceLifetime.Scoped*/ );

            services.AddScoped(typeof(IStoreIdentityDbInitializer), typeof(StoreIdentityDbInitializer));

            #endregion

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));

            //services.AddIdentityCore<ApplicationUser>()
            //    .AddRoles<IdentityRole>();

            return services;

        }
    }
}
