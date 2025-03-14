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
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        public async Task<int> Create(GameDTO game)
        {
            var mappedService = _mapper.Map<Game>(game);
            if(mappedService != null)
            {
                await _gameRepository.Create(mappedService);
                return mappedService.ID;
            }
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int id)
        {
            return await _gameRepository.Delete(id);
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
