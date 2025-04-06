using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Infrastructure.Repositories.InMemoryRepositories
{
    class InMemoryReviewRepository : IReviewRepository
    {
        private List<Review> _reviews = new List<Review>();

        public InMemoryReviewRepository()
        {
            _reviews = new List<Review>
            {
                new Review 
                {
                    ID = 1, GameId = 1, UserId = 1, Score = 8, Name = "TOP", Content = "Very good game" 
                } 
            };
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

        public Task<bool> DeleteByGameId(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAllByGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAllByUser(int userId)
        {
            throw new NotImplementedException();
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
