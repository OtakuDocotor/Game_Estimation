using Application.DTO;
using Application.Exceptions;
using Application.Requests.GameRequests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
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

        public GameService(IGameRepository gameRepository, IMapper mapper, IDeveloperRepository developerRepository, IReviewRepository reviewRepository, NpgsqlConnection connection)
        {
            _reviewRepository = reviewRepository;
            _developerRepository = developerRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
            _connection = connection;
        }

        public async Task<int> Create(CreateGameRequest request)
        {
            var game = new Game()
            { 
                Name = request.Name,
                AverageScore = request.AverageScore,
                DeveloperId = request.DeveloperId
            };

            return await _gameRepository.Create(game);
        }

        public async Task Delete(int id)
        {
            await _connection.OpenAsync();
            await using var transaction = await _connection.BeginTransactionAsync();

            try
            {
                await _reviewRepository.DeleteByGameId(id);

                var deleteResult = await _gameRepository.Delete(id);
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
            var game = new Game()
            {
                ID = request.ID,
                Name = request.Name,
                AverageScore = request.AverageScore,
                DeveloperId = request.DeveloperId
            };

            var updateResult = await _gameRepository.Update(game);
            if (!updateResult)
            {
                throw new EntityUpdateException("Game not delete");
            }
        }
    }
}
