using Application.Mappings;
using Application.Services;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Moq;
using FluentAssertions;
using Bogus;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Application.Exceptions;
using Application.Requests.ReviewRequests;
using Application.DTO;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace ApplicationUnitTests.Services
{
    public class ReviewServiceTests
    {
        private IReviewService _reviewService;
        private readonly IMapper _mapper;
        private readonly IReviewRepository _reviewRepository;
        private readonly Mock<ILogger<ReviewService>> _loggerMock;
        private Faker _faker;
        private Mock<IReviewRepository> _mockReviewRepository;

        public ReviewServiceTests()
        {
            _faker = new Faker();

            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = mappingConfig.CreateMapper();

            var reviewRepositoryMock = new Mock<IReviewRepository>();
            _mockReviewRepository = reviewRepositoryMock;

            _loggerMock = new Mock<ILogger<ReviewService>>();

            _reviewService = new ReviewService(_reviewRepository, _mapper, _loggerMock.Object);
        }

        [Fact]
        public void ShouldBeAvailableToCreate()
        {
            _reviewService.Should().NotBeNull();
        }

        [Fact]
        public async Task Create_NullInput_Returns_Exception()
        {
            _reviewService = new ReviewService(_reviewRepository, _mapper, _loggerMock.Object);
            await Assert.ThrowsAnyAsync<NullReferenceException>(() =>
            _reviewService.Create(null));
            _mockReviewRepository.Verify(x => x.Create(It.IsAny<Review>()), Times.Never);

        }

        [Fact]
        public async void Create_ValidRequest_ReturnsId()
        {
            _mockReviewRepository.Setup(x => x.Create(It.IsAny<Review>())).ReturnsAsync(1);
            _reviewService = new ReviewService(_mockReviewRepository.Object, _mapper, _loggerMock.Object);

            var validReview = new CreateReviewRequest{
            Name = _faker.Random.String(1,50),
            Content = _faker.Random.String(1,500),
            UserId = _faker.Random.Int(1,100),
            GameId = _faker.Random.Int(1,100),
            Score = _faker.Random.Int(1,10)};


            var result = await _reviewService.Create(validReview);

            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Delete_WhenReviewNotFound_ThrowsException()
        {

            int nonExistentId = 999;
            _mockReviewRepository
                .Setup(x => x.Delete(nonExistentId));

            _reviewService = new ReviewService(_mockReviewRepository.Object, _mapper, _loggerMock.Object);

            await Assert.ThrowsAsync<EntityDeleteException>(() =>
                _reviewService.Delete(nonExistentId));
        }

        [Fact]
        public async Task ReadById_WhenReviewNotFound_ThrowsNotFoundException()
        {
            int nonExistentId = 999;

            _mockReviewRepository
                .Setup(x => x.ReadById(nonExistentId))
                .ReturnsAsync((Review)null);

            _reviewService = new ReviewService(_mockReviewRepository.Object, _mapper, _loggerMock.Object);

            await Assert.ThrowsAsync<NotFoundApplicationException>(() =>
                _reviewService.ReadById(nonExistentId));
        }

        [Fact]
        public async Task ReadById_WhenReviewExists_ReturnsReview()
        {
            int existingId = _faker.Random.Int(1, 100);
            var review = new Review
            {
                ID = existingId,
                Name = "Test Review",
                Content = "Test Content",
                Score= _faker.Random.Int(1, 10),
                GameId = _faker.Random.Int(1, 100),
                UserId = _faker.Random.Int(1, 100)
            };

            _mockReviewRepository.Setup(x => x.ReadById(existingId)).ReturnsAsync(review);
            _reviewService = new ReviewService(_mockReviewRepository.Object, _mapper, _loggerMock.Object);

            var result = await _reviewService.ReadById(existingId);

            result.Should().NotBeNull();
            result.ID.Should().Be(review.ID);
            result.Score.Should().Be(review.Score);
            result.UserId.Should().Be(review.UserId);
            result.GameId.Should().Be(review.GameId);
            _mockReviewRepository.Verify(x => x.ReadById(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void Update_ValidRequest_UpdateReview()
        {
            int existingId = _faker.Random.Int(1, 100);
            var request = new UpdateReviewRequest
            {
                ID = existingId,
                Name = "Test Review1",
                Content = "Test Content",
                Score = _faker.Random.Int(1, 10),
                GameId = _faker.Random.Int(1, 100),
                UserId = _faker.Random.Int(1, 100)
            };

            var review = new Review
            {
                ID = existingId,
                Name = "Test Review",
                Content = "Test Content",
                Score = _faker.Random.Int(1, 10),
                GameId = _faker.Random.Int(1, 100),
                UserId = _faker.Random.Int(1, 100)
            };

            _mockReviewRepository.Setup(x => x.ReadById(existingId)).ReturnsAsync(review);
            _mockReviewRepository.Setup(x => x.Update(It.IsAny<Review>())).ReturnsAsync(true);
            _reviewService = new ReviewService(_mockReviewRepository.Object, _mapper, _loggerMock.Object);

            await _reviewService.Update(request);

            _mockReviewRepository.Verify(x => x.Update(It.Is<Review>(r =>
                    r.ID == request.ID &&
                    r.Score == request.Score &&
                    r.Content == request.Content &&
                    r.GameId == request.GameId &&
                    r.UserId == request.UserId)), Times.Once);
        }

    }
}
