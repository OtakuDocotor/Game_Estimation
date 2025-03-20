using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IGameRepository
    {
        public Task<Game?> ReadById(int id);
        public Task<IEnumerable<Game>> ReadAll();
        public Task<int> Create(Game game);
        public Task<bool> Update(Game game);
        public Task<bool> Delete(int id);
    }
}
