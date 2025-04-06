using Application.DTO;
using Application.Exceptions;
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
            var review = new Review()
            {
                Name = request.Name,
                Content = request.Content,
                GameId = request.GameId,
                UserId = request.UserId 
            };

            return await _reviewRepository.Create(review);
        }

        public async Task Delete(int id)
        {
            var deleteResult = await _reviewRepository.Delete(id);
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

        public async Task<ReviewDTO?> ReadById(int id)
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
            var review = new Review()
            {
                ID = request.ID,
                Name = request.Name,
                Content = request.Content,
                GameId = request.GameId,
                UserId = request.UserId
            };

            var updateResult = await _reviewRepository.Update(review);
            if (!updateResult)
            {
                throw new EntityUpdateException("Review not update");
            }
        }
    }
}
