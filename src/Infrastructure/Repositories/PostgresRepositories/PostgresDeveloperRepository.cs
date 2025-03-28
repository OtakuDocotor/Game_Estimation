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
            await _connection.OpenAsync();
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
            await _connection.CloseAsync();
            return developerId;
        }

        public async Task<bool> Delete(int id)
        {
            await _connection.OpenAsync();
            var affectedRows = await _connection.ExecuteAsync(
                @"DELETE FROM developers
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            await _connection.CloseAsync();
            return affectedRows > 0;
        }

        public async Task<IEnumerable<Developer>> ReadAll()
        {
            await _connection.OpenAsync();
            var developers = await _connection.QueryAsync<Developer>(
                @"SELECT id, name, description, logo_url
                FROM developers");
            await _connection.CloseAsync();
            return developers;
        }

        public async Task<Developer?> ReadById(int id)
        {
            await _connection.OpenAsync();
            var developer = await _connection.QueryFirstOrDefaultAsync<Developer>(
                @"SELECT id, name, description, logo_url
                FROM developers
                WHERE id = @Id",
                new
                {
                    Id = id
                });
            await _connection.CloseAsync();
            return developer;
        }

        public async Task<bool> Update(Developer dev)
        {
            await _connection.OpenAsync();
            var affectedRows = await _connection.ExecuteAsync(
                @"UPDATE developers SET 
                id = @Id
                name = @Name
                description = @Description
                logo_url = @Logo_URL",
            new
            {
                Id = dev.ID,
                Name = dev.Name,
                Description = dev.Description,
                Logo_URL = dev.LogoURL
            });
            return affectedRows>0;
        }
    }
}
