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
        public GameService(IGameRepository gameRepository, IMapper mapper,IDeveloperRepository developerRepository,IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
            _developerRepository = developerRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        public async Task<int> Create(GameDTO game)
        {
            var mappedService = _mapper.Map<Game>(game);
            if(mappedService != null && await _developerRepository.ReadById(game.DeveloperId)!=null)
            {
                await _gameRepository.Create(mappedService);
                return mappedService.ID;
            }
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int id)
        {
            var game = await ReadById(id);
            if (game != null)
            {
                game.Reviews.ForEach(async x => await _reviewRepository.Delete(x.ID));
                return await _gameRepository.Delete(id);
            }
            throw new ArgumentNullException();
        }
        public async Task<List<GameDTO>> ReadAll()
        {
            var games = await _gameRepository.ReadAll();
            var mappedServices = games.Select(x => _mapper.Map<GameDTO>(x)).ToList();
            return mappedServices;
        }
        public async Task<GameDTO?> ReadById(int id)
        {
            var game = await _gameRepository.ReadById(id);
            var mappedService = _mapper.Map<GameDTO>(game);
            return mappedService;
        }
        public async Task<bool> Update(GameDTO game)
        {
            var mappedService = _mapper.Map<Game>(game);
            if (mappedService != null)
            {
                return await _gameRepository.Update(mappedService);
            }
            throw  new NotImplementedException();
        }
    }
}
