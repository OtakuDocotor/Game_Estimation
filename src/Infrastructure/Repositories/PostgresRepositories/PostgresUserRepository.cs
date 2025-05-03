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
                @"INSERT INTO users (name, email, password_hash, role)
                VALUES (@Name, @Email, @PasswordHash, @Role::user_role)
                RETURNING id",
                new
                {
                    Name = user.Name,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Role = user.Role.ToString() 
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
                @"SELECT id, name, email, password_hash, role
                FROM users");
            return users;
        }

        public async Task<User?> ReadByEmail(string email)
        {
            var user = await _connection.QueryFirstOrDefaultAsync<User>(
                @"SELECT id, name, email, password_hash, role
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
                @"SELECT id, name, email, password_hash, role
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
                name = @Name,
                email = @Email, 
                password_hash = @PasswordHash, 
                role = @Role
                WHERE id = @Id",
            new
            {
                Id = user.ID,
                Name = user.Name,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            });
            return affectedRows > 0;
        }
    }
}
