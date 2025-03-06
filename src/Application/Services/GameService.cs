using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    class GameService : IGameService
    {
        public Task<int> Create(GameDTO game)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GameDTO>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<GameDTO?> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(GameDTO game)
        {
            throw new NotImplementedException();
        }
    }
}
