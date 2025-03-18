using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IUserService
    {
        public Task<List<UserDTO?>> ReadById(int id);
        public Task<List<UserDTO>> ReadAll();
        public Task<int> Create(UserDTO user);
        public Task<bool> Update(UserDTO user);
        public Task<bool> Delete(int id);
    }
}
