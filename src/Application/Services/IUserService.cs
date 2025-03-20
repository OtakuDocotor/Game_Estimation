using Application.DTO;

namespace Application.Services
{
    public interface IUserService
    {
        public Task<UserDTO?> ReadById(int id);
        public Task<IEnumerable<UserDTO>> ReadAll();
        public Task<int> Create(UserDTO user);
        public Task<bool> Update(UserDTO user);
        public Task<bool> Delete(int id);
    }
}
