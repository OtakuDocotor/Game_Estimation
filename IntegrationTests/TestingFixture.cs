using System.Reflection;
using Application;
using Bogus;
using Domain.Entities;
using Domain.Enums;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors;
using Infrastructure;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Respawn;
using MigrationRunner = Infrastructure.Database.MigrationRunner.MigrationRunner;

namespace ApplicationIntegrationTests;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class TestingFixture : IAsyncLifetime
{
    private readonly Faker _faker;

    private Respawner _respawner = null!;

    public TestingFixture()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) => { config.AddJsonFile("appsettings.json"); })
            .ConfigureServices((context, services) =>
            {
                services.AddInfrastructure();
                services.AddApplication();

                var connectionString = context.Configuration.GetConnectionString("PostgresDB");
                if (string.IsNullOrWhiteSpace(connectionString))
                    throw new ApplicationException("PostgresDBIntegration connection string is empty");

                services.AddSingleton(_ => new NpgsqlDataSourceBuilder(connectionString).Build());

                services.AddTransient(sp =>
                {
                    var dataSource = sp.GetRequiredService<NpgsqlDataSource>();
                    return dataSource.CreateConnection();
                });

                services
                    .AddFluentMigratorCore()
                    .ConfigureRunner(rb => rb
                        .AddPostgres()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations())
                    .Configure<SelectingProcessorAccessorOptions>(options => { options.ProcessorId = "Postgres"; });
            })
            .Build();

        ServiceProvider = host.Services;

        _faker = new Faker();
    }
    public async Task<Developer> CreateDeveloper()
    {
        using var scope = ServiceProvider.CreateScope();
        var developerRepository = scope.ServiceProvider.GetRequiredService<IDeveloperRepository>();

        var developerId = await developerRepository.Create(new Developer
        {
            Name = _faker.Name.FirstName(),
            Description = _faker.Random.String2(100),
            LogoURL = _faker.Random.String2(15)
        });

        var developer = await developerRepository.ReadById(developerId);
        if (developer == null) throw new Exception("Can't create developer");
        return developer;
    }

    public async Task<Game> CreateGame()
    {
        using var scope = ServiceProvider.CreateScope();
        var gameRepository = scope.ServiceProvider.GetRequiredService<IGameRepository>();
        var devId = (await CreateDeveloper()).ID;
        var gameId = await gameRepository.Create(new Game
        {
            Name = _faker.Name.FirstName(),
            DeveloperId = devId,
            AverageScore = 10

        });

        var game = await gameRepository.ReadById(gameId);
        if (game == null) throw new Exception("Can't create game");
        return game;
    }

    public async Task<User> CreateUser()
    {
        using var scope = ServiceProvider.CreateScope();
        var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();

        var userId = await userRepository.Create(new User
        {
            Name = _faker.Name.FirstName(),
            Email = _faker.Internet.Email(),
            PasswordHash = "625x68235x578346x5786734aasdas",
            Role = UserRoles.User
        });

        var user = await userRepository.ReadById(userId);
        if (user == null) throw new Exception("Can't create user");
        return user;
    }

    public async Task<Review> CreateReview()
    {
        using var scope = ServiceProvider.CreateScope();
        var reviewRepository = scope.ServiceProvider.GetRequiredService<IReviewRepository>();
        var game = await CreateGame();
        var user = await CreateUser();
        var reviewId = await reviewRepository.Create(new Review
        {
            Name = "Test Review",
            Content = "Test Content",
            Score = 10,
            GameId = game.ID,
            UserId = user.ID

        });

        var review = await reviewRepository.ReadById(reviewId);
        if (review == null) throw new Exception("Can't create review");
        return review;
    }

    public IServiceProvider ServiceProvider { get; }

    public async Task InitializeAsync()
    {
        using var scope = ServiceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<NpgsqlConnection>();
        await connection.OpenAsync();

        var migrationRunner = scope.ServiceProvider.GetRequiredService<MigrationRunner>();
        migrationRunner.Run();

        _respawner = await Respawner.CreateAsync(connection, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = ["public"],
            TablesToIgnore = ["VersionInfo"]
        });
    }

    public async Task DisposeAsync()
    {
        await ResetDatabase();
    }

    private async Task ResetDatabase()
    {
        using var scope = ServiceProvider.CreateScope();
        var connection = scope.ServiceProvider.GetRequiredService<NpgsqlConnection>();
        await connection.OpenAsync();

        await _respawner.ResetAsync(connection);
    }
}