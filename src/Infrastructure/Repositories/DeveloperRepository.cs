using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    class DeveloperRepository : IDeveloperRepository
    {
        private List<Developer> _developers = new List<Developer>();

        public DeveloperRepository()
        {
            _developers = new List<Developer> { 
                new Developer { ID = 1, Name = "Arcane", Description = "Good dev",
                    Games = new List<Game> {
                        new Game { ID = 1, DeveloperId = 1, Name = "Prey",AverageScore=8,Reviews=
                            new List<Review>{ 
                                new Review {ID=1,GameId=1,UserId=1,Score=8,Name="TOP",Content="Very good game" } } } },LogoURL="asdadasdadsda" }};
        }

        public Task<int> Create(Developer dev)
        {
            _developers.Add(dev);
            return Task.FromResult(dev.ID);
        }

        public Task<bool> Delete(int id)
        {
            if (!_developers.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            _developers.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Developer>> ReadAll()
        {
            return Task.FromResult(_developers.AsEnumerable());
        }

        public Task<IEnumerable<Developer>?> ReadById(int id)
        {
            var dev = _developers.FindAll(x => x.ID == id);
            return Task.FromResult(dev.AsEnumerable());
        }

        public Task<bool> Update(Developer dev)
        {
            var devToUpd = _developers.Find(x => x.ID == dev.ID);
            if (devToUpd == null)
            {
                return Task.FromResult(false);
            }
            devToUpd.Name = dev.Name;
            devToUpd.Games = dev.Games;
            devToUpd.Description = dev.Description;
            devToUpd.LogoURL = dev.LogoURL;
            return Task.FromResult(true);
        }
    }
}
