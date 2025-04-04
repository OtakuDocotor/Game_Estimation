using Application.DTO;
using Application.Requests.DeveloperRequests;

namespace Application.Services
{
    public interface IDeveloperService
    {
        public Task<DeveloperDTO?> ReadById(int id);
        public Task<IEnumerable<DeveloperDTO>> ReadAll();
        public Task<int> Create(CreateDeveloperRequest dev);
        public Task<bool> Update(UpdateDeveloperRequest dev);
        public Task<bool> Delete(int id);
    }
}
