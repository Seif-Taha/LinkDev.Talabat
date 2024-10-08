using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services) 
        {

            return services;

        }

    }
}
