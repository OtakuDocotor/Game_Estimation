using Application.DTO;
using Application.Requests.GameRequests;

namespace Application.Services
{
    public interface IGameService
    {
        public Task<GameDTO?> ReadById(int id);
        public Task<IEnumerable<GameDTO>> ReadAll();
        public Task<int> Create(CreateGameRequest game);
        public Task Update(UpdateGameRequest game);
        public Task Delete(int id);
    }
}
