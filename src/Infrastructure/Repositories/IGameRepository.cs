using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IGameRepository
    {
        public Task<Game?> ReadById(int id);
        public Task<List<Game>> ReadAll();
        public Task<int> Create(Game game);
        public Task<bool> Update(Game game);
        public Task<bool> Delete(int id);
    }
}
