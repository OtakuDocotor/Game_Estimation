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
        private List<Game> games = new List<Game>();
        public GameRepository()
        {

        }
        public Task Create(Game game)
        {
            games.Add(game);
            return Task.CompletedTask;
        }

        public Task<bool> Delete(int id)
        {
            if(games.Any(x=>x.ID==id))
            {
                return Task.FromResult(false);
            }
            games.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<List<Game>> ReadAll()
        {
            return Task.FromResult(games);
        }

        public Task<Game?> ReadById(int id)
        {
            var game = games.Find(x => x.ID == id);
            return Task.FromResult(game);
        }

        public Task<bool> Update(Game game)
        {
            var gameToUpt = games.Find(x => x.ID == game.ID);
            if (gameToUpt==null)
            {
                return Task.FromResult(false);
            }
            gameToUpt._Developer = game._Developer;
            gameToUpt.Reviews = game.Reviews;
            gameToUpt.AVG_Estamalation = game.AVG_Estamalation;
            gameToUpt.Name = game.Name;
            return Task.FromResult(true);
        }
    }
}
