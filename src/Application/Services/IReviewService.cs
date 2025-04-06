using Application.DTO;
using Application.Requests.ReviewRequests;

namespace Application.Services
{
    public interface IReviewService
    {
        public Task<ReviewDTO?> ReadById(int id);
        public Task<IEnumerable<ReviewDTO>> ReadAll();
        public Task<int> Create(CreateReviewRequest review);
        public Task Update(UpdateReviewRequest review);
        public Task Delete(int id);
    }
}
