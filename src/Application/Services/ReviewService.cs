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
        private IMapper _mapper;
        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task<int> Create(ReviewDTO review)
        {
            var mappedService = _mapper.Map<Review>(review);
            if(mappedService!=null)
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
            return await _reviewRepository.Update(mappedService);
        }
    }
}
