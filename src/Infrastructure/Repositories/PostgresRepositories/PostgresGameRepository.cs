using Dapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Repositories.PostgressRepositories
{
    public class PostgresGameRepository : IGameRepository
    {
        private readonly NpgsqlConnection _connection;

        public PostgresGameRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Create(Game game)
        {
            await _connection.OpenAsync();
            var gameId = await _connection.QuerySingleAsync<int>(
                @"INSERT INTO games (name, average_score, developer_id)
                VALUES (@Name, @AverageScore, @DeveloperId)
                RETURNING id",
                new
                {
                    Name = game.Name,
                    AverageScore = game.AverageScore,
                    DeveloperId = game.DeveloperId
                });
            await _connection.CloseAsync();
            return gameId;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.OpenAsync();
            var affectedRows = await _connection.ExecuteAsync(
                @"DELETE FROM games
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            await _connection.CloseAsync();
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Game>> ReadAll()
        {
            await _connection.OpenAsync();
            var games = await _connection.QueryAsync<Game>(
                @"SELECT id, name, average_score, developer_id
                FROM games");
            await _connection.CloseAsync();
            return games;
        }

        public async Task<Game?> ReadById(int id)
        {
            await _connection.OpenAsync();
            var game = await _connection.QueryFirstOrDefaultAsync<Game>(
                @"SELECT id, name, average_score, developer_id
                FROM games
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            await _connection.CloseAsync();
            return game;
        }

        public async Task<bool> Update(Game game)
        {
            await _connection.OpenAsync();
            var affectedRows = await _connection.ExecuteAsync(
                @"UPDATE games SET 
                name = @Name
                average_score = @AverageScore
                developer_Id = @DeveloperId
                WHERE id = @Id",
            new
            {
                Id = game.ID,
                Name = game.Name,
                AverageScore = game.AverageScore,
                DeveloperId = game.DeveloperId
            });
            return affectedRows > 0;
        }
    }
}
