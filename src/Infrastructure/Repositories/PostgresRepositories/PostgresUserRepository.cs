using Dapper;
using Domain.Entities;
using Infrastructure.Database.TypeMappings;
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
            const string query =@"
                INSERT INTO users (name, email, password_hash, role)
                VALUES (@Name, @Email, @PasswordHash, @Role::user_role)
                RETURNING id";
            return await _connection.ExecuteScalarAsync<int>(query, user.AsDapperParams());
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
                @"SELECT id, name, email, password_hash, role::text
                FROM users");
            return users;
        }

        public async Task<User?> ReadByEmail(string email)
        {
            var user = await _connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT id, name, email, password_hash, role::text
                FROM users
                WHERE email = @Email",
                new
                {
                    Email = email
                });
            return user;
        }

        public async Task<User?> ReadById(int id)
        {
            var user = await _connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT id, name, email, password_hash, role::text
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
            const string query = @"
                UPDATE users SET 
                name = @Name,
                email = @Email, 
                password_hash = @PasswordHash, 
                role = @Role::user_role
                WHERE id = @Id";
            var affectedRows = await _connection.ExecuteAsync(query, user.AsDapperParams());
            return affectedRows > 0;
        }
    }
}
