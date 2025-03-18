using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using Infrastructure.Repositories;
using AutoMapper;
using Domain.Entities;
using System.Runtime.InteropServices;

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
                await _gameRepository.Create(mappedGame);
                return mappedGame.ID;
            }
            return 0;
        }

        public async Task<bool> Delete(int id)
        {
            var games = await ReadById(id);
            if (games != null)
            {
                games.ForEach(async x =>
                {
                    var reviews = (await _reviewRepository.ReadAll()).ToList();
                    var reviewsToDelete = reviews.FindAll(x => x.UserId == id);
                    if (reviewsToDelete != null)
                    {
                        reviewsToDelete.ForEach(async x => await _reviewRepository.Delete(x.ID));
                    }
                });
                return await _gameRepository.Delete(id);
            }
            return false;
        }

        public async Task<List<GameDTO>> ReadAll()
        {
            var games = await _gameRepository.ReadAll();
            var mappedGames = games.Select(x => _mapper.Map<GameDTO>(x)).ToList();
            return mappedGames;
        }

        public async Task<List<GameDTO>?> ReadById(int id)
        {
            var games = await _gameRepository.ReadById(id);
            var mappedGames = games.Select(x => _mapper.Map<GameDTO>(x)).ToList();
            return mappedGames;
        }

        public async Task<bool> Update(GameDTO game)
        {
            var mappedGames = _mapper.Map<Game>(game);
            if (mappedGames != null)
            {
                return await _gameRepository.Update(mappedGames);
            }
            throw  new NotImplementedException();
        }
    }
}
