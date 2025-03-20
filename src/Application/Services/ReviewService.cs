using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper, IUserRepository userRepository, IGameRepository gameRepository)
        {
            _reviewRepository = reviewRepository;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(ReviewDTO review)
        {
            var mappedReview = _mapper.Map<Review>(review);
            var gameExists = await _gameRepository.ReadById(review.GameId) != null;
            var userExists = await _userRepository.ReadById(review.UserId) != null;
            if (mappedReview != null && gameExists && userExists)
            {
               var id = await _reviewRepository.Create(mappedReview);
                return id;
            }
            return 0;
        }

        public async Task<bool> Delete(int id)
        {
            return await _reviewRepository.Delete(id);
        }

        public async Task<IEnumerable<ReviewDTO>> ReadAll()
        {
            var reviews = await _reviewRepository.ReadAll();
            var mappedReviews = reviews.Select(x => _mapper.Map<ReviewDTO>(x));
            return mappedReviews;
        }

        public async Task<ReviewDTO?> ReadById(int id)
        {
            var review = await _reviewRepository.ReadById(id);
            var mappedReview =_mapper.Map<ReviewDTO>(review);
            return mappedReview;
        }

        public async Task<bool> Update(ReviewDTO review)
        {
            var mappedReview = _mapper.Map<Review>(review);
            if (mappedReview != null)
            {
                return await _reviewRepository.Update(mappedReview);
            }
            return false;
        }
    }
}
