using Application.DTO;
using Application.Requests.UserRequest;

namespace Application.Services
{
    public interface IUserService
    {
        public Task<UserDTO?> ReadById(int id);
        public Task<IEnumerable<UserDTO>> ReadAll();
        public Task<int> Create(CreateUserRequest user);
        public Task<bool> Update(UpdateUserRequest user);
        public Task<bool> Delete(int id);
    }
}
