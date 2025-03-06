using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    class DeveloperService : IDeveloperService
    {
        public Task<int> Create(DeveloperDTO dev)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DeveloperDTO>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<DeveloperDTO?> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(DeveloperDTO dev)
        {
            throw new NotImplementedException();
        }
    }
}
