using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public interface IDeveloperRepository
    {
        public Task<Developer?> ReadById(int id);
        public Task<List<Developer>> ReadAll();
        public Task Create(Developer dev);
        public Task<bool> Update(Developer dev);
        public Task<bool> Delete(int id);
    }
}
