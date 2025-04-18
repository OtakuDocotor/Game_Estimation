using Application.DTO;
using Application.Exceptions;
using Application.Requests.GameRequests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<GameService> _logger;

        public GameService(IGameRepository gameRepository, IMapper mapper, IDeveloperRepository developerRepository, IReviewRepository reviewRepository, NpgsqlConnection connection, ILogger<GameService> logger)
        {
            _reviewRepository = reviewRepository;
            _developerRepository = developerRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _connection = connection;
            _logger = logger;
        }

        public async Task<int> Create(CreateGameRequest request)
        {
            var game = new Game()
            { 
                Name = request.Name,
                AverageScore = request.AverageScore,
                DeveloperId = request.DeveloperId
            };

            var createResult = await _gameRepository.Create(game);
            _logger.LogInformation("Game created with id:{ID}", createResult);
            return createResult;
        }

        public async Task Delete(int id)
        {
            await _connection.OpenAsync();
            await using var transaction = await _connection.BeginTransactionAsync();

            try
            {
                await _reviewRepository.DeleteByGameId(id);

                var deleteResult = await _gameRepository.Delete(id);
                _logger.LogInformation("Game with id:{id}, deleted", id);
                if (!deleteResult)
                {
                    throw new InvalidOperationException("Game not deleted");
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new EntityDeleteException($"Error deleting game {id}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<GameDTO>> ReadAll()
        {
            var games = await _gameRepository.ReadAll();
            var mappedGames = games.Select(x => _mapper.Map<GameDTO>(x));
            return mappedGames;
        }

        public async Task<GameDTO> ReadById(int id)
        {
            var game = await _gameRepository.ReadById(id);
            if (game == null)
            {
                throw new NotFoundApplicationException("Game not found");
            }
            var mappedGame = _mapper.Map<GameDTO>(game);
            return mappedGame;
        }

        public async Task Update(UpdateGameRequest request)
        {
            var game = await _gameRepository.ReadById(request.ID);
            game.ChangeName(request.Name);
            game.ChangeDeveloper(request.DeveloperId);
            game.ChangeAvgScore(request.AverageScore);

            var updateResult = await _gameRepository.Update(game);
            if (!updateResult)
            {
                throw new EntityUpdateException("Game not delete");
            }
            _logger.LogInformation("Game with id:{id}, updated", request.ID);
        }
    }
}
