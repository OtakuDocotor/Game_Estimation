using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IReviewRepository
    {
        public Task<Review?> ReadById(int id);
        public Task<IEnumerable<Review>> ReadAll();
        public Task<int> Create(Review review);
        public Task<bool> Update(Review review);
        public Task<bool> Delete(int id);
    }
}
