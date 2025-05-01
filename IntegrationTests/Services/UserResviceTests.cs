using Application.Services;
using ApplicationIntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Application.Requests;
using Bogus;
using Application.Exceptions;
using Application.Requests.UserRequest;


namespace ApplicationUnitTests.Services
{
    public class UserResviceTests : IClassFixture<TestingFixture>
    {
        private readonly TestingFixture _fixture;
        private readonly IUserService _userService;

        public UserResviceTests(TestingFixture fixture)
        {
            _fixture = fixture;
            _userService = fixture.ServiceProvider.GetRequiredService<IUserService>();
        }

        [Fact]
        public async Task Create_ShouldCreateUser_AndReturnId()
        {
            var request = new CreateUserRequest
            {
                Name = "Ivan"
            };

            var userId = await _userService.Create(request);
            userId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ReadBy_ShouldReturnUser_WhenExists()
        {
            var user = await _fixture.CreateUser();
            var result = await _userService.ReadById(user.ID);

            result.Should().NotBeNull();
            result.ID.Should().Be(user.ID);
        }

        [Fact]
        public async Task ReadAll_ShouldReturnAllUsers()
        {
            await _fixture.CreateUser();
            await _fixture.CreateUser();

            var result = await _userService.ReadAll();
            result.Count().Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Update_ShouldModifyUser()
        {
            var user = await _fixture.CreateUser();
            var request = new UpdateUserRequest
            {
                ID = user.ID,
                Name = "Updated User Name"
            };

            await _userService.Update(request);
            var result = await _userService.ReadById(request.ID);

            result.ID.Should().Be(request.ID);
            result.Name.Should().Be(request.Name);
        }
    }
}
