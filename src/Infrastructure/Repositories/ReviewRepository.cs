using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class ReviewRepository : IReviewRepository
    {
        private List<Review> Reviews = new List<Review>();
        public ReviewRepository()
        {

        }
        public Task Create(Review review)
        {
            Reviews.Add(review);
            return Task.CompletedTask;
        }

        public Task<bool> Delete(int id)
        {
            if (Reviews.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            Reviews.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<List<Review>> ReadAll()
        {
            return Task.FromResult(Reviews);
        }

        public Task<Review?> ReadById(int id)
        {
            var rev = Reviews.Find(x => x.ID == id);
            return Task.FromResult(rev);
        }

        public Task<bool> Update(Review review)
        {
            var revToUpt = Reviews.Find(x => x.ID == review.ID);
            if (revToUpt == null)
            {
                return Task.FromResult(false);
            }
            revToUpt.Write_On = review.Write_On;
            revToUpt.Name = review.Name;
            revToUpt.Author = review.Author;
            revToUpt.Content = review.Content;
            return Task.FromResult(true);
        }
    }
}
