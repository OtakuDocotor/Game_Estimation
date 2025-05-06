using Application.Requests.UserRequests;
using Application.Responses;
using Application.Services;
using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Api.Controllers;
using FluentAssertions;

namespace ApplicationUnitTests.Controllers
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;
        private readonly Faker _faker = new();

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object)
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                }
            };
        }

        [Fact]
        public async Task Register_ValidRequest_ReturnsCreatedStatus()
        {
            var request = new RegistrationRequest
            {
                Name = _faker.Name.FirstName(),
                Email = _faker.Internet.Email(),
                Password = "eve@12345678"
            };
            const int expectedUserId = 1;
            _authServiceMock.Setup(x => x.Register(request))
                          .ReturnsAsync(expectedUserId);

            var result = await _controller.Register(request);
   
            result.Should().BeOfType<CreatedResult>();
            _authServiceMock.Verify(x => x.Register(request), Times.Once);
        }

        [Fact]
        public async Task Register_ServiceThrowsException_Exception()
        {
            var request = new RegistrationRequest
            {
                Name = _faker.Name.FirstName(),
                Email = _faker.Internet.Email(),
                Password = "eve@12345678"
            };
            _authServiceMock.Setup(x => x.Register(request))
                .ThrowsAsync(new Exception("Error"));


            await _controller.Invoking(async x => await x.Register(request))
                .Should().ThrowAsync<Exception>()
                .WithMessage("Error");
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            var request = new LoginRequest
            {
                Email = _faker.Internet.Email(),
                Password = "eve@12345678"
            };
            var expectedResponse = new LoginResponse { Token = "test" };
            _authServiceMock.Setup(x => x.Login(request)).ReturnsAsync(expectedResponse);

            var result = await _controller.Login(request);

            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(expectedResponse);

            _authServiceMock.Verify(x => x.Login(request), Times.Once);
        }

        [Fact]
        public async Task Login_InvalidCredentials_UnauthorizedException()
        {
            var request = new LoginRequest
            {
                Email = _faker.Internet.Email(),
                Password = "eve@123"
            };
            _authServiceMock.Setup(x => x.Login(request))
                .ThrowsAsync(new UnauthorizedAccessException("Invalid credentials"));

            await _controller.Invoking(async x => await x.Login(request))
                .Should().ThrowAsync<UnauthorizedAccessException>();
        }
    }
}
