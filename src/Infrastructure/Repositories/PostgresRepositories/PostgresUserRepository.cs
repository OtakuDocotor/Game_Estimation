using Dapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Repositories.PostgressRepositories
{
    class PostgresUserRepository : IUserRepository
    {
        private readonly NpgsqlConnection _connection;

        public PostgresUserRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Create(User user)
        {
            var userId = await _connection.QuerySingleAsync<int>(
                @"INSERT INTO users (name)
                VALUES (@Name)
                RETURNING id",
                new
                {
                    Name = user.Name,
                });
            return userId;
        }

        public async Task<bool> Delete(int id)
        {
            var affectedRows = await _connection.ExecuteAsync(
                @"DELETE FROM users
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<User>> ReadAll()
        {
            var users = await _connection.QueryAsync<User>(
                @"SELECT id, name
                FROM users");
            return users;
        }

        public async Task<User?> ReadById(int id)
        {
            var user = await _connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT id, name
                FROM users
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            return user;
        }

        public async Task<bool> Update(User user)
        {
            var affectedRows = await _connection.ExecuteAsync(
                @"UPDATE users SET 
                name = @Name
                WHERE id = @Id",
            new
            {
                Id = user.ID,
                Name = user.Name
            });
            return affectedRows > 0;
        }
    }
}
