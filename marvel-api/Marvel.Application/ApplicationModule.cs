using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marvel.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.SetIoC();

            return services;
        }

        public static IServiceCollection SetIoC(this IServiceCollection services)
        {
            services.AddScoped<ICharacterApplication, CharacterApplication>();

            return services;
        }
    }
}
