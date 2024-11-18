using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? databaseConnection, IConfigurationSection? jwtConfigurationSection)
        {
            services.AddAutoMapper(typeof(DependencyInjection));

            if (jwtConfigurationSection is not null)
            {
                services.Configure<JwtConfiguration>(jwtConfigurationSection);
            }

            services.AddDbContext<CinemaContext>(opt => opt.UseSqlServer(databaseConnection));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            services.AddScoped<IUserRolesService, UserRolesService>();

            services.AddScoped<IAuthenticateRepository, AuthenticateRepository>();
            services.AddScoped<IAuthenticateService, AuthenticateService>();

            services.AddScoped<ICinemaService, CinemaService>();
            services.AddScoped<ICinemaRepository, CinemaRepository>();

            services.AddScoped<IMovieService, MovieService>();
            services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IMovieGenderService, MovieGenderService>();
            services.AddScoped<IMovieGenderRepository, MovieGenderRepository>();

            services.AddScoped<IMovieClassificationService, MovieClassificationService>();
            services.AddScoped<IMovieClassificationRepository, MovieClassificationRepository>();

            services.AddScoped<IAddressesService, AddressesService>();
            services.AddScoped<IAddressesRepository, AddressesRepository>();

            services.AddScoped<IMovieScreenService, MovieByScreenService>();
            services.AddScoped<IMovieScreenRepository, MovieScreenRepository>();

            services.AddScoped<ICinemaScreenService, CinemaScreenService>();
            services.AddScoped<ICinemaScreenRepository, CinemaScreenRepository>();

            return services;
        }
    }
}
