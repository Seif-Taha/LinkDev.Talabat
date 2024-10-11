﻿using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {
            
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IServiceManager), typeof(ServiceManager));

            return services;

        }

    }
}
