using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IDeveloperRepository
    {
        public Task<Developer?> ReadById(int id);
        public Task<IEnumerable<Developer>> ReadAll();
        public Task<int> Create(Developer dev);
        public Task<bool> Update(Developer dev);
        public Task<bool> Delete(int id);
    }
}
