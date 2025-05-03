using Application.Services;
using ApplicationIntegrationTests;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Requests.UserRequests;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IntegrationTests.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<IPasswordHasher> _passwordHasherMock = new();
        private readonly Mock<IMapper> _mapperMock = new();

        public AuthServiceTests()
        {
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["JwtSettings:Secret"] = "super-secret-key-at-least-32-chars-long",
                    ["JwtSettings:Issuer"] = "test-issuer",
                    ["JwtSettings:Audience"] = "test-audience",
                    ["JwtSettings:ExpirationInMinutes"] = "30"
                })
                .Build();

            _authService = new AuthService(
                config,
                _mapperMock.Object,
                _userRepositoryMock.Object,
                _passwordHasherMock.Object);
        }

        [Fact]
        public async Task Register_ValidRequest_CreatesUserWithUserRole()
        {
            var request = new RegistrationRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "Password123!"
            };

            var expectedUser = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = "hashed-password",
                Role = UserRoles.User
            };

            _mapperMock.Setup(m => m.Map<User>(request))
                     .Returns(expectedUser);

            _passwordHasherMock.Setup(h => h.HashPassword(request.Password))
                             .Returns("hashed-password");

            _userRepositoryMock.Setup(r => r.Create(It.Is<User>(u =>
                u.Role == UserRoles.User &&
                u.PasswordHash == "hashed-password")))
                .ReturnsAsync(1);

            var userId = await _authService.Register(request);

            userId.Should().Be(1);
            _mapperMock.Verify(m => m.Map<User>(request), Times.Once);
            _passwordHasherMock.Verify(h => h.HashPassword(request.Password), Times.Once);
            _userRepositoryMock.Verify(r => r.Create(expectedUser), Times.Once);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsJwtToken()
        {
            var request = new LoginRequest
            {
                Email = "valid@example.com",
                Password = "correct-password"
            };

            var existingUser = new User
            {
                ID = 1,
                Email = request.Email,
                PasswordHash = "hashed-password",
                Role = UserRoles.Admin
            };

            _userRepositoryMock.Setup(r => r.ReadByEmail(request.Email))
                             .ReturnsAsync(existingUser);

            _passwordHasherMock.Setup(h => h.VerifyPassword(
                request.Password, existingUser.PasswordHash))
                             .Returns(true);

            var result = await _authService.Login(request);

            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Login_InvalidCredentials_ThrowsUnauthorizedException()
        {
            var request = new LoginRequest
            {
                Email = "invalid@example.com",
                Password = "wrong-password"
            };

            _userRepositoryMock.Setup(r => r.ReadByEmail(request.Email))
                             .ReturnsAsync((User?)null);

            _passwordHasherMock.Setup(h => h.VerifyPassword(
                It.IsAny<string>(), It.IsAny<string>()))
                             .Returns(false);

            await Assert.ThrowsAsync<UnauthorizedAccessException>(
                () => _authService.Login(request));
        }
    }
}
