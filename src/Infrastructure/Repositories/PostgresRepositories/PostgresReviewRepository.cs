using Dapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Repositories.PostgressRepositories
{
    class PostgresReviewRepository : IReviewRepository
    {
        private readonly NpgsqlConnection _connection;

        public PostgresReviewRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Create(Review review)
        {
            var reviewId = await _connection.QuerySingleAsync<int>(
                @"INSERT INTO reviews (name, content, score, user_id, game_id)
                VALUES (@Name, @Content, @Score, @UserId, @GameId)
                RETURNING id",
                new
                {
                    Name = review.Name,
                    Content = review.Content,
                    Score = review.Score,
                    UserId = review.UserId,
                    GameId = review.GameId
                });
            return reviewId;
        }

        public async Task<bool> Delete(int id)
        {
            var affectedRows = await _connection.ExecuteAsync(
                @"DELETE FROM reviews
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Review>> ReadAll()
        {
            var reviews = await _connection.QueryAsync<Review>(
                @"SELECT id, name, content, score, user_id, game_id
                FROM reviews");
            return reviews;
        }

        public async Task<Review?> ReadById(int id)
        {
            var review = await _connection.QueryFirstOrDefaultAsync<Review>(
                @"SELECT id, name, content, score, user_id, game_id
                FROM reviews
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            return review;
        }

        public async Task<bool> Update(Review review)
        {
            var affectedRows = await _connection.ExecuteAsync(
                @"UPDATE reviews SET 
                name = @Name,
                content = @Content,
                score = @Score,
                user_id = @UserId,
                game_id = @GameId
                WHERE id=@Id ",
            new
            {
                Id = review.ID,
                Name = review.Name,
                Content = review.Content,
                Score = review.Score,
                UserId = review.UserId,
                GameId = review.GameId
            });
            return affectedRows > 0;
        }
    }
}
