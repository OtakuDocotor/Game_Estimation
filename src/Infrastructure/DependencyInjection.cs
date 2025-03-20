using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IDeveloperRepository, DeveloperRepository>();
            services.AddSingleton<IGameRepository, GameRepository>();
            services.AddSingleton<IReviewRepository, ReviewRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();
            return services;
        }
    }
}
