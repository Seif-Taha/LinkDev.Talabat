using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Abstraction.Basket;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            //services.AddScoped(typeof(IBasketService), typeof(BasketService));
            //services.AddScoped(typeof(Func<IBasketService>), typeof(Func<BasketService>));

            services.AddScoped(typeof(Func<IBasketService>), (servicesProvider) =>
            {
                var mapper = servicesProvider.GetRequiredService<IMapper>();
                var configuration = servicesProvider.GetRequiredService<IConfiguration>();
                var basketRepoistory = servicesProvider.GetRequiredService<IBasketRepoistory>();

                return () => new BasketService(basketRepoistory, mapper, configuration);

            });

            return services;

        }

    }
}
