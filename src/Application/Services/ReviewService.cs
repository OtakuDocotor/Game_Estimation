using Application.DTO;
using Application.Exceptions;
using Application.Requests.ReviewRequests;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ReviewService> _logger;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper, IUserRepository userRepository, IGameRepository gameRepository, ILogger<ReviewService> logger)
        {
            _reviewRepository = reviewRepository;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Create(CreateReviewRequest request)
        {
            var review = new Review()
            {
                Name = request.Name,
                Content = request.Content,
                GameId = request.GameId,
                UserId = request.UserId 
            };

            var createResult = await _reviewRepository.Create(review);
            _logger.LogInformation("Review created with id:{ID}", createResult);
            return createResult;
        }

        public async Task Delete(int id)
        {
            var deleteResult = await _reviewRepository.Delete(id);
            _logger.LogInformation("Review with id:{id}, deleted", id);
            if (!deleteResult)
            {
                throw new EntityDeleteException("Review not deleted");
            }
        }

        public async Task<IEnumerable<ReviewDTO>> ReadAll()
        {
            var reviews = await _reviewRepository.ReadAll();
            var mappedReviews = reviews.Select(x => _mapper.Map<ReviewDTO>(x));
            return mappedReviews;
        }

        public async Task<ReviewDTO> ReadById(int id)
        {
            var review = await _reviewRepository.ReadById(id);
            if (review == null)
            {
                throw new NotFoundApplicationException("Review not found");
            }
            var mappedReview =_mapper.Map<ReviewDTO>(review);
            return mappedReview;
        }

        public async Task Update(UpdateReviewRequest request)
        {
            var review = await _reviewRepository.ReadById(request.ID);
            review.ChangeName(request.Name);
            review.ChangeContent(request.Content);
            review.ChangeUser(request.UserId);
            review.ChangeGame(request.GameId);
            review.ChangeScore(request.Score);

            var updateResult = await _reviewRepository.Update(review);
            if (!updateResult)
            {
                throw new EntityUpdateException("Review not update");
            }
            _logger.LogInformation("Review with id:{id}, updated", request.ID);
        }
    }
}
