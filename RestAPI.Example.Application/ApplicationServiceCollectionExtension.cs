
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RestAPI.Example.Application.Database;
using RestAPI.Example.Application.Respositories;
using RestAPI.Example.Application.Services;

namespace RestAPI.Example.Application
{
    public static class ApplicationServiceCollectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddSingleton<IMovieRepository, MovieRepository>();
            services.AddSingleton<IMovieService, MovieService>();
            services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDBConnectionFactory>(_ => new SqlConnectionFactory(connectionString));
            services.AddSingleton<DBInitilizer>();

            return services;
        }
    }
}
