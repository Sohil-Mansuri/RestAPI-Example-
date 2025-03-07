

using Microsoft.Extensions.DependencyInjection;
using RestAPI.Example.Application.Respositories;

namespace RestAPI.Example.Application
{
    public static class ApplicationServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MovieRepository>();
            return services;
        }
    }
}
