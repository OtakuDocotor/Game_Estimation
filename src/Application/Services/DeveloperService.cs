using Application.DTO;
using Application.Exceptions;
using Application.Requests.DeveloperRequests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<DeveloperService> _logger;

        public DeveloperService(IDeveloperRepository developerRepository, IMapper mapper, IGameRepository gameRepository, IReviewRepository reviewRepository, NpgsqlConnection connection, ILogger<DeveloperService> logger)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
            _reviewRepository = reviewRepository;
            _connection = connection;
            _logger = logger;
        }

        public async Task<int> Create(CreateDeveloperRequest request)
        {
            var developer = new Developer
            {
                Name = request.Name,
                Description = request.Description,
                LogoURL = request.LogoURL
            };

            var createResult = await _developerRepository.Create(developer);
            _logger.LogInformation("Developer created with id:{ID}", createResult);
            return createResult;
        }

        public async Task Delete(int id)
        {
            await _connection.OpenAsync();
            await using var transaction = await _connection.BeginTransactionAsync();

            try
            {
                var gamesToDelete = (await _gameRepository.GamesIdByDeveloper(id)).ToArray();
                if (gamesToDelete != null)
                {
                    await _reviewRepository.DeleteByGames(gamesToDelete);
                    await _gameRepository.DeleteByDeveloper(id);
                }
                var deleteResult = await _developerRepository.Delete(id);
                _logger.LogInformation("Developer with id:{id}, deleted", id);
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
            var developer = await _developerRepository.ReadById(request.ID);
            developer.ChangeName(request.Name);
            developer.ChangeDescription(request.Description);
            developer.ChangeLogo(request.LogoURL);

            var updateResult = await _developerRepository.Update(developer);
            if (!updateResult)
            {
                throw new EntityUpdateException("Developer not updated.");
            }
            _logger.LogInformation("Developer with id:{id}, updated", request.ID);
        }
    }
}
