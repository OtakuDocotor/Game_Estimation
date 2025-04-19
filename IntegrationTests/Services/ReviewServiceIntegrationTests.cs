using Application.Services;
using ApplicationIntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Application.Requests.ReviewRequests;
using Bogus;
using Application.Exceptions;

namespace IntegrationTests.Services
{
    public class ReviewServiceIntegrationTests :IClassFixture<TestingFixture>
    {
        private readonly TestingFixture _fixture;
        private readonly IReviewService _reviewService;

        public ReviewServiceIntegrationTests(TestingFixture fixture)
        {
            _fixture = fixture;
            _reviewService = fixture.ServiceProvider.GetRequiredService<IReviewService>();
        }
        [Fact]
        public async Task Create_ShouldCreateReview_AndReturnId()
        {
            var game = await _fixture.CreateGame();
            var user = await _fixture.CreateUser();
            var request = new CreateReviewRequest
            {
                Name = "Test Review",
                Content = "Test Content",
                Score = 10,
                GameId = game.ID,
                UserId = user.ID
            };

            var apartmentId = await _reviewService.Create(request);
            apartmentId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ReadBy_ShouldReturnReview_WhenExists()
        {
            var review = await _fixture.CreateReview();
            var result = await _reviewService.ReadById(review.ID);


            result.Should().NotBeNull();
            result.ID.Should().Be(review.ID);
        }

        [Fact]
        public async Task ReadAll_ShouldReturnAllReviews()
        {
            await _fixture.CreateReview();
            await _fixture.CreateReview();

            var result = await _reviewService.ReadAll();
            result.Count().Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Update_ShouldModifyReviews()
        {
            var review = await _fixture.CreateReview();
            var game = await _fixture.CreateGame();
            var user = await _fixture.CreateUser();
            var request = new UpdateReviewRequest
            {
                ID = review.ID,
                Name = "Updated Test Review",
                Content = "Updated Test Content",
                Score = 1,
                GameId = game.ID ,
                UserId = user.ID
            };

            await _reviewService.Update(request);
            var result =await _reviewService.ReadById(request.ID);
            result.ID.Should().Be(request.ID);
            result.Name.Should().Be(request.Name);
            result.Content.Should().Be(request.Content);
            result.Score.Should().Be(request.Score);
            result.GameId.Should().Be(request.GameId);
            result.UserId.Should().Be(request.UserId);
        }
    }
}
