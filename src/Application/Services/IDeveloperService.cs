using Application.DTO;

namespace Application.Services
{
    public interface IDeveloperService
    {
        public Task<DeveloperDTO?> ReadById(int id);
        public Task<IEnumerable<DeveloperDTO>> ReadAll();
        public Task<int> Create(DeveloperDTO dev);
        public Task<bool> Update(DeveloperDTO dev);
        public Task<bool> Delete(int id);
    }
}
