using Application.DTO;
using Application.Requests.ReviewRequests;
using AutoMapper;
using Azure.Core;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;

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

        public async Task<int> Create(CreateReviewRequest request)
        {
            var gameExists = await _gameRepository.ReadById(request.GameId) != null;
            var userExists = await _userRepository.ReadById(request.UserId) != null;
            var review = new Review()
            {
                Name = request.Name,
                Content = request.Content,
                GameId = request.GameId,
                UserId = request.UserId 
            };

            return await _reviewRepository.Create(review);
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

        public async Task<bool> Update(UpdateReviewRequest request)
        {
            var review = new Review()
            {
                ID = request.ID,
                Name = request.Name,
                Content = request.Content,
                GameId = request.GameId,
                UserId = request.UserId
            };
            return await _reviewRepository.Update(review);
        }
    }
}
