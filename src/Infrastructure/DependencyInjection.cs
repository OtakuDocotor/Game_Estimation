using FluentMigrator.Runner;
using Infrastructure.Repositories.Interfaces;
using Infrastructure.Repositories.PostgressRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionStrings = configuration.GetConnectionString("PostgresDB");
                return new NpgsqlDataSourceBuilder(connectionStrings).Build();
            });

            services.AddScoped(sp =>
            {
                var dataSource = sp.GetRequiredService<NpgsqlDataSource>();
                return dataSource.CreateConnection();
            });

            services.AddTransient<IDeveloperRepository, PostgresDeveloperRepository>();
            services.AddTransient<IUserRepository, PostgresUserRepository>();
            services.AddTransient<IGameRepository, PostgresGameRepository>();
            services.AddTransient<IReviewRepository, PostgresReviewRepository>();

            DapperConfig.Configure();
            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                .AddPostgres()
                .WithGlobalConnectionString("PostgresDB")
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
            services.AddScoped<Database.MigrationRunner.MigrationRunner>();

            return services;
        }
    }
}
