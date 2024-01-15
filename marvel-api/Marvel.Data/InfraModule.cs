using Marvel.Infra.Context;
using Marvel.Infra.Repository;
using Marvel.Infra.Repository.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Marvel.Infra
{
    public static class InfraModule
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.SetIoC();
            return services;
        }

        public static IServiceCollection SetIoC(this IServiceCollection services)
        {
            services.AddScoped<IMarvelService, MarvelService>();
            services.AddSingleton<MarvelDbContext>();
            services.AddScoped<IFavoriteMarvelCharacterRepository, FavoriteMarvelCharacterRepository>();

            return services;
        }
    }
}
