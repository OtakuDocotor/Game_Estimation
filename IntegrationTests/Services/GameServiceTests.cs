using Application.Services;
using ApplicationIntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Application.Requests;
using Bogus;
using Application.Exceptions;
using Application.Requests.UserRequest;
using Application.Requests.GameRequests;

namespace ApplicationUnitTests.Services
{
    public class GameServiceTests : IClassFixture<TestingFixture>
    {
        private readonly TestingFixture _fixture;
        private readonly IGameService _gameService;

        public GameServiceTests(TestingFixture fixture)
        {
            _fixture = fixture;
            _gameService = fixture.ServiceProvider.GetRequiredService<IGameService>();
        }
        [Fact]
        public async Task Create_ShouldCreateGame_AndReturnId()
        {
            var developer = await _fixture.CreateDeveloper();
            var request = new CreateGameRequest
            {
                Name = "Nigthmare",
                DeveloperId = developer.ID,
                AverageScore = 10
            };

            var gameId = await _gameService.Create(request);
            gameId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ReadBy_ShouldReturnGame_WhenExists()
        {
            var game = await _fixture.CreateGame();
            var result = await _gameService.ReadById(game.ID);

            result.Should().NotBeNull();
            result.ID.Should().Be(game.ID);
        }

        [Fact]
        public async Task ReadAll_ShouldReturnAllGames()
        {
            await _fixture.CreateGame();
            await _fixture.CreateGame();

            var result = await _gameService.ReadAll();
            result.Count().Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Update_ShouldModifyGame()
        {
            var game = await _fixture.CreateGame();
            var newDeveloper = await _fixture.CreateDeveloper();
            var request = new UpdateGameRequest
            {
                ID = game.ID,
                Name = "Updated Game Name",
                DeveloperId = newDeveloper.ID,
                AverageScore = 5
            };

            await _gameService.Update(request);
            var result = await _gameService.ReadById(request.ID);

            result.ID.Should().Be(request.ID);
            result.Name.Should().Be(request.Name);
            result.DeveloperId.Should().Be(request.DeveloperId);
            result.AverageScore.Should().Be(request.AverageScore);
        }
    }
}
