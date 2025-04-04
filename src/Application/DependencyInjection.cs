using Microsoft.Extensions.DependencyInjection;
using Application.Mappings;
using Application.Services;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Reflection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IDeveloperService, DeveloperService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IUserService, UserService>();

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
