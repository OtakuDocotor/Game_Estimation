using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IReviewService
    {
        public Task<ReviewDTO?> ReadById(int id);
        public Task<IEnumerable<ReviewDTO>> ReadAll();
        public Task<int> Create(ReviewDTO review);
        public Task<bool> Update(ReviewDTO review);
        public Task<bool> Delete(int id);
    }
}
