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
        private List<Developer> Developers = new List<Developer>();
        public DeveloperRepository()
        {

        }
        public Task Create(Developer dev)
        {
            Developers.Add(dev);
            return Task.CompletedTask;
        }

        public Task<bool> Delete(int id)
        {
            if (Developers.Any(x => x.ID == id))
            {
                return Task.FromResult(false);
            }
            Developers.RemoveAll(x => x.ID == id);
            return Task.FromResult(true);
        }

        public Task<List<Developer>> ReadAll()
        {
            return Task.FromResult(Developers);
        }

        public Task<Developer?> ReadById(int id)
        {
            var dev = Developers.Find(x => x.ID == id);
            return Task.FromResult(dev);
        }

        public Task<bool> Update(Developer dev)
        {
            var devToUpt = Developers.Find(x => x.ID == dev.ID);
            if (devToUpt == null)
            {
                return Task.FromResult(false);
            }
            devToUpt.Name = dev.Name;
            devToUpt.Games = dev.Games;
            devToUpt.Content = dev.Content;
            return Task.FromResult(true);
        }
    }
}
