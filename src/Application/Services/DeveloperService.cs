using Application.DTO;
using Application.Exceptions;
using Application.Requests.DeveloperRequests;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

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

        public async Task<int> Create(CreateDeveloperRequest request)
        {
            var developer = new Developer
            {
                Name = request.Name,
                Description = request.Description,
                LogoURL = request.LogoURL
            };
            return await _developerRepository.Create(developer);
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

                var deleteResult = await _developerRepository.Delete(id);
                if (!deleteResult)
                {
                    throw new EntityDeleteException("Developer not deleted");
                }
                return deleteResult;
            }
            throw new EntityDeleteException("Developer not deleted");
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
            if (developer == null)
            {
                throw new NotFoundApplicationException("Developer not found");
            }
            var mappedDeveloper = _mapper.Map<DeveloperDTO>(developer);
            return mappedDeveloper;
        }

        public async Task<bool> Update(UpdateDeveloperRequest request)
        {
            var developer = new Developer
            {
                ID = request.ID,
                Name = request.Name,
                Description = request.Description,
                LogoURL = request.LogoURL
            };

            var updateResult = await _developerRepository.Update(developer);
            if (!updateResult)
            {
                throw new EntityUpdateException("Developer not updated.");
            }
            return updateResult;
        }
    }
}
