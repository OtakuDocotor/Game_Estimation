using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    class UserService : IUserService
    {
        public Task Create(UserDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDTO>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO?> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
