using Application.DTO;
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
            var mappedDeveloper = _mapper.Map<Developer>(dev);
            if (mappedDeveloper != null)
            {
                var id = await _developerRepository.Create(mappedDeveloper);
                return id;
            }
            return 0;
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
            return false;
        }

        public async Task<IEnumerable<DeveloperDTO>> ReadAll()
        {
            var developers = await _developerRepository.ReadAll();
            var mappedDeveloper = developers.Select(x => _mapper.Map<DeveloperDTO>(x));
            return mappedDeveloper;
        }

        public async Task<DeveloperDTO?> ReadById(int id)
        {
            var developer = await _developerRepository.ReadById(id);
            var mappedDeveloper = _mapper.Map<DeveloperDTO>(developer);
            return mappedDeveloper;
        }

        public async Task<bool> Update(DeveloperDTO dev)
        {
            var mappedDeveloper = _mapper.Map<Developer>(dev);
            if (mappedDeveloper != null)
            {
                return await _developerRepository.Update(mappedDeveloper);
            }
            return false;
        }
    }
}
