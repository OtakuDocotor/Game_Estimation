using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IReviewRepository
    {
        public Task<IEnumerable<Review>?> ReadById(int id);
        public Task<IEnumerable<Review>> ReadAll();
        public Task<int> Create(Review review);
        public Task<bool> Update(Review review);
        public Task<bool> Delete(int id);
    }
}
