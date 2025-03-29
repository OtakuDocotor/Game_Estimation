using Dapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Npgsql;

namespace Infrastructure.Repositories.PostgressRepositories
{
    public class PostgresDeveloperRepository : IDeveloperRepository
    {
        private readonly NpgsqlConnection _connection;

        public PostgresDeveloperRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<int> Create(Developer dev)
        {
            var developerId = await _connection.QuerySingleAsync<int>(
                @"INSERT INTO developers (name, description, logo_url)
                VALUES (@Name, @Description, @LogoURL)
                RETURNING id",
                new
                {
                    Name = dev.Name,
                    Description = dev.Description,
                    LogoURL = dev.LogoURL
                });
            return developerId;
        }

        public async Task<bool> Delete(int id)
        {
            var affectedRows = await _connection.ExecuteAsync(
                @"DELETE FROM developers
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Developer>> ReadAll()
        {
            var developers = await _connection.QueryAsync<Developer>(
                @"SELECT id, name, description, logo_url
                FROM developers");
            return developers;
        }

        public async Task<Developer?> ReadById(int id)
        {
            var developer = await _connection.QueryFirstOrDefaultAsync<Developer>(
                @"SELECT id, name, description, logo_url
                FROM developers
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            return developer;
        }

        public async Task<bool> Update(Developer dev)
        {
            var affectedRows = await _connection.ExecuteAsync(
                @"UPDATE developers SET 
                name = @Name
                description = @Description
                logo_url = @LogoURL
                WHERE id = @Id",
                dev);
            return affectedRows > 0;
        }
    }
}
