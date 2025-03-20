using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IReviewRepository reviewRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _reviewRepository = reviewRepository;
        }

        public async Task<int> Create(UserDTO user)
        {
            var mappedUser = _mapper.Map<User>(user);
            if (mappedUser != null)
            {
                var id = await _userRepository.Create(mappedUser);
                return id;
            }
            return 0;
        }

        public async Task<bool> Delete(int id)
        {
            var user = await ReadById(id);
            if (user != null)
            {
                var reviews = (await _reviewRepository.ReadAll()).ToList();
                var reviewsToDelete = reviews.FindAll(x => x.UserId == id);
                if (reviewsToDelete != null)
                {
                    reviewsToDelete.ForEach(async x => await _reviewRepository.Delete(x.ID));
                }
            }
            return await _userRepository.Delete(id);
        }

        public async Task<IEnumerable<UserDTO>> ReadAll()
        {
            var users = await _userRepository.ReadAll();
            var mappedUsers = users.Select(x => _mapper.Map<UserDTO>(x));
            return mappedUsers;
        }

        public async Task<UserDTO?> ReadById(int id)
        {
            var user = await _userRepository.ReadById(id);
            var mappedUser = _mapper.Map<UserDTO>(user);
            return mappedUser;
        }

        public async Task<bool> Update(UserDTO user)
        {
            var mappedUser = _mapper.Map<User>(user);
            if (mappedUser != null)
            {
                return await _userRepository.Update(mappedUser);
            }
            return false;
        }
    }
}
