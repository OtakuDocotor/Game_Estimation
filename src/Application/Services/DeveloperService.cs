using Application.DTO;
using Application.Exceptions;
using Application.Requests.DeveloperRequests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Npgsql;

namespace Application.Services
{
    public class DeveloperService : IDeveloperService
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly NpgsqlConnection _connection;

        public DeveloperService(IDeveloperRepository developerRepository, IMapper mapper, IGameRepository gameRepository, IReviewRepository reviewRepository,NpgsqlConnection connection)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
            _reviewRepository = reviewRepository;
            _connection = connection;
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

        public async Task Delete(int id)
        {
            await _connection.OpenAsync();
            await using var transaction = await _connection.BeginTransactionAsync();

            try
            {
                var gamesToDelete = (await _gameRepository.GamesByDeveloper(id)).ToList();
                if (gamesToDelete != null)
                    gamesToDelete.ForEach(async x =>
                    {
                        await _reviewRepository.DeleteByGameId(x.ID);
                        await _gameRepository.Delete(x.ID);
                    });

                var deleteResult = await _developerRepository.Delete(id);
                if (!deleteResult)
                {
                    throw new InvalidOperationException("Developer not deleted");
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new EntityDeleteException($"Error deleting developer {id}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<DeveloperDTO>> ReadAll()
        {
            var developers = await _developerRepository.ReadAll();
            var mappedDeveloper = developers.Select(x => _mapper.Map<DeveloperDTO>(x));
            return mappedDeveloper;
        }

        public async Task<DeveloperDTO> ReadById(int id)
        {
            var developer = await _developerRepository.ReadById(id);
            if (developer == null)
            {
                throw new NotFoundApplicationException("Developer not found");
            }
            var mappedDeveloper = _mapper.Map<DeveloperDTO>(developer);
            return mappedDeveloper;
        }

        public async Task Update(UpdateDeveloperRequest request)
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
        }
    }
}
