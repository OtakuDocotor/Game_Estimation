using Application.DTO;
using Application.Exceptions;
using Application.Requests.UserRequest;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly NpgsqlConnection _connection;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, IMapper mapper, IReviewRepository reviewRepository, NpgsqlConnection connection, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
            _connection = connection;
            _logger = logger;
        }

        public async Task<int> Create(CreateUserRequest request)
        {
            var user = new User
            {
                Name = request.Name
            };

            var createResult = await _userRepository.Create(user);
            _logger.LogInformation("User created with id:{ID}", createResult);
            return createResult;
        }

        public async Task Delete(int id)
        {
            await _connection.OpenAsync();
            using var transaction = await _connection.BeginTransactionAsync();

            try
            {
                await _reviewRepository.DeleteByUserId(id);

                var deleteResult = await _userRepository.Delete(id);
                _logger.LogInformation("User with id:{id}, deleted", id);
                if (!deleteResult)
                {
                    throw new EntityDeleteException("User not delete");
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw new EntityDeleteException($"Error deleting user {id}");
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IEnumerable<UserDTO>> ReadAll()
        {
            var users = await _userRepository.ReadAll();
            var mappedUsers = users.Select(x => _mapper.Map<UserDTO>(x));
            return mappedUsers;
        }

        public async Task<UserDTO> ReadById(int id)
        {
            var user = await _userRepository.ReadById(id);
            var mappedUser = _mapper.Map<UserDTO>(user);
            return mappedUser;
        }

        public async Task Update(UpdateUserRequest request)
        {
            var user = await _userRepository.ReadById(request.ID);
            user.ChangeName(request.Name);

            var updateResult = await _userRepository.Update(user);
            if (!updateResult)
            {
                throw new EntityUpdateException("User not updated");
            }
            _logger.LogInformation("User with id:{id}, updated", request.ID);
        }
    }
}
