using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository, IMapper mapper,IUserRepository userRepository,IGameRepository gameRepository)
        {
            _reviewRepository = reviewRepository;
            _gameRepository = gameRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<int> Create(ReviewDTO review)
        {
            var mappedService = _mapper.Map<Review>(review);
            if(mappedService != null&&(await _gameRepository.ReadById(review.GameId)==null)&&(await _userRepository.ReadById(review.UserId)==null))
            {
                await _reviewRepository.Create(mappedService);
                return mappedService.ID;
            }
            throw new NotImplementedException();
        }
        public async Task<bool> Delete(int id)
        {
            return await _reviewRepository.Delete(id);
        }

        public async Task<List<ReviewDTO>> ReadAll()
        {
            var reviews =await _reviewRepository.ReadAll();
            var mappedServices = reviews.Select(x => _mapper.Map<ReviewDTO>(x)).ToList();
            return mappedServices;
        }
        public async Task<ReviewDTO?> ReadById(int id)
        {
            var review = await _reviewRepository.ReadById(id);
            var mappedService = _mapper.Map<ReviewDTO>(review);
            return mappedService;
        }
        public async Task<bool> Update(ReviewDTO review)
        {
            var mappedService = _mapper.Map<Review>(review);
            if (mappedService != null)
            {
                return await _reviewRepository.Update(mappedService);
            }
            throw new NotImplementedException();
        }
    }
}
