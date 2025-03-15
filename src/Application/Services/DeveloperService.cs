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

namespace Application.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        public DeveloperService(IDeveloperRepository developerRepository, IMapper mapper, IGameRepository gameRepository, IReviewRepository reviewRepository)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<int> Create(DeveloperDTO dev)
        {
            var mappedService = _mapper.Map<Developer>(dev);
            if(mappedService != null)
            {
                await _developerRepository.Create(mappedService);
                return mappedService.ID;
            }
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int id)
        {
            var developer = await ReadById(id);
            if (developer != null)
            {
                developer.Games.ForEach(async x =>
                {
                    x.Reviews.ForEach(async y => await _reviewRepository.Delete(y.ID));
                    await _gameRepository.Delete(x.ID);
                });
                return await _developerRepository.Delete(id);
            }
            throw new ArgumentNullException();
        }
        public async Task<List<DeveloperDTO>> ReadAll()
        {
            var developers = await _developerRepository.ReadAll();
            var mappedServices = developers.Select(x => _mapper.Map<DeveloperDTO>(x)).ToList();
            return mappedServices;
        }
        public async Task<DeveloperDTO?> ReadById(int id)
        {
            var developer = await _developerRepository.ReadById(id);
            var mappedService = _mapper.Map<DeveloperDTO>(developer);
            return mappedService;
        }
        public async Task<bool> Update(DeveloperDTO dev)
        {
            var mappedService = _mapper.Map<Developer>(dev);
            if (mappedService != null)
            {
                return await _developerRepository.Update(mappedService);
            }
            throw new NotImplementedException();
        }
    }
}
