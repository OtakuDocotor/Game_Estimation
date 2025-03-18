using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(UserDTO user)
        {
            var mappedUser = _mapper.Map<User>(user);
            if (mappedUser != null)
            {
                await _userRepository.Create(mappedUser);
                return mappedUser.ID;
            }
            return 0;
        }

        public async Task<bool> Delete(int id)
        {
            return await _userRepository.Delete(id);
        }

        public async Task<List<UserDTO>> ReadAll()
        {
            var users = await _userRepository.ReadAll();
            var mappedServices = users.Select(x => _mapper.Map<UserDTO>(x)).ToList();
            return mappedServices;
        }

        public async Task<List<UserDTO?>> ReadById(int id)
        {
            var users = await _userRepository.ReadById(id);
            var mappedService = users.Select(x => _mapper.Map<UserDTO>(x)).ToList();
            return mappedService;
        }

        public async Task<bool> Update(UserDTO user)
        {
            var mappedService = _mapper.Map<User>(user);
            if (mappedService != null)
            {
                return await _userRepository.Update(mappedService);
            }
            throw new NotImplementedException();
        }
    }
}
