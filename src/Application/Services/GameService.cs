using Application.DTO;
using Application.Exceptions;
using Application.Requests.GameRequests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Application.Services
{
    public class GameService : IGameService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GameService(IGameRepository gameRepository, IMapper mapper, IDeveloperRepository developerRepository, IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
            _developerRepository = developerRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
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

        public async Task<bool> Delete(int id)
        {
            var game = await ReadById(id);
            if (game != null)
            {
                var reviewsToDelete = (await _reviewRepository.GetAllByGame(id)).ToList();
                if (reviewsToDelete != null)
                {
                    reviewsToDelete.ForEach(async x => await _reviewRepository.Delete(x.ID));
                }

                var deleteResult = await _gameRepository.Delete(id);
                if (!deleteResult)
                {
                    throw new EntityDeleteException("Game not deleted");
                }
                return deleteResult;
            }
            throw new EntityDeleteException("Game not deleted");
        }

        public async Task<IEnumerable<GameDTO>> ReadAll()
        {
            var games = await _gameRepository.ReadAll();
            var mappedGames = games.Select(x => _mapper.Map<GameDTO>(x));
            return mappedGames;
        }

        public async Task<GameDTO?> ReadById(int id)
        {
            var game = await _gameRepository.ReadById(id);
            if (game == null)
            {
                throw new NotFoundApplicationException("Game not found");
            }
            var mappedGame = _mapper.Map<GameDTO>(game);
            return mappedGame;
        }

        public async Task<bool> Update(UpdateGameRequest request)
        {
            var game = new Game()
            {
                ID = request.ID,
                Name = request.Name,
                AverageScore = request.AverageScore,
                DeveloperId = request.DeveloperId
            };

            var upadateResult = await _gameRepository.Update(game);
            if (!upadateResult)
            {
                throw new EntityUpdateException("Game not delete");
            }
            return upadateResult;
        }
    }
}
