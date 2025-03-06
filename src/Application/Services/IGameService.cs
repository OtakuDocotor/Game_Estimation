using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IGameService
    {
        public Task<GameDTO?> ReadById(int id);
        public Task<List<GameDTO>> ReadAll();
        public Task Create(GameDTO game);
        public Task<bool> Update(GameDTO game);
        public Task<bool> Delete(int id);
    }
}
