using Application.DTO;
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

        public async Task<int> Create(GameDTO game)
        {
            var mappedGame = _mapper.Map<Game>(game);
            if (mappedGame != null && await _developerRepository.ReadById(game.DeveloperId) != null)
            {
                var id = await _gameRepository.Create(mappedGame);
                return id;
            }
            return 0;
        }

        public async Task<bool> Delete(int id)
        {
            var game = await ReadById(id);
            if (game != null)
            {
                var reviews = (await _reviewRepository.ReadAll()).ToList();
                var reviewsToDelete = reviews.FindAll(x => x.GameId == id);
                if (reviewsToDelete != null)
                {
                    reviewsToDelete.ForEach(async x => await _reviewRepository.Delete(x.ID));
                }
                return await _gameRepository.Delete(id);
            }
            return false;
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
            var mappedGame = _mapper.Map<GameDTO>(game);
            return mappedGame;
        }

        public async Task<bool> Update(GameDTO game)
        {
            var mappedGames = _mapper.Map<Game>(game);
            if (mappedGames != null)
            {
                return await _gameRepository.Update(mappedGames);
            }
            return false;
        }
    }
}
