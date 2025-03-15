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
        private List<Review> _reviews = new List<Review>();
        public ReviewRepository()
        {
            _reviews = new List<Review>{
                            new Review {ID=1,GameId=1,UserId=1,Score=8,Name="TOP",Content="Very good game" } };
        }
        public Task<int> Create(Review review)
        {
            _reviews.Add(review);
            return Task.FromResult(review.ID);
        }

        public Task<bool> Delete(int id)
        {
            if (!_reviews.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            _reviews.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Review>> ReadAll()
        {
            return Task.FromResult(_reviews.AsEnumerable());
        }

        public Task<Review?> ReadById(int id)
        {
            var rev = _reviews.Find(x => x.ID == id);
            return Task.FromResult(rev);
        }

        public Task<bool> Update(Review review)
        {
            var revToUpd = _reviews.Find(x => x.ID == review.ID);
            if (revToUpd == null)
            {
                return Task.FromResult(false);
            }
            revToUpd.UserId = review.UserId;
            revToUpd.Name = review.Name;
            revToUpd.GameId = review.GameId;
            revToUpd.Content = review.Content;
            revToUpd.Score = review.Score;
            return Task.FromResult(true);
        }
    }
}
