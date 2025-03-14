using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class GameRepository : IGameRepository
    {
        private List<Game> _games = new List<Game>();
        public GameRepository()
        {
        }
        public Task<int> Create(Game game)
        {
            _games.Add(game);
            return Task.FromResult(game.ID);
        }

        public Task<bool> Delete(int id)
        {
            if(!_games.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            _games.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Game>> ReadAll()
        {
            return Task.FromResult(_games.AsEnumerable());
        }

        public Task<Game?> ReadById(int id)
        {
            var game = _games.Find(x => x.ID == id);
            return Task.FromResult(game);
        }

        public Task<bool> Update(Game game)
        {
            var gameToUpd = _games.Find(x => x.ID == game.ID);
            if (gameToUpd==null)
            {
                return Task.FromResult(false);
            }
            gameToUpd.Developer = game.Developer;
            gameToUpd.Reviews = game.Reviews;
            gameToUpd.AverageScore = game.AverageScore;
            gameToUpd.Name = game.Name;
            return Task.FromResult(true);
        }
    }
}
