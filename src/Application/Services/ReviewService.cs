using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    class ReviewService : IReviewService
    {
        public Task<int> Create(ReviewDTO review)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReviewDTO>> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Task<ReviewDTO?> ReadById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ReviewDTO review)
        {
            throw new NotImplementedException();
        }
    }
}
