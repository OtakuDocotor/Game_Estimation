using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IDeveloperService
    {
        public Task<DeveloperDTO?> ReadById(int id);
        public Task<IEnumerable<DeveloperDTO>> ReadAll();
        public Task<int> Create(DeveloperDTO dev);
        public Task<bool> Update(DeveloperDTO dev);
        public Task<bool> Delete(int id);
    }
}
