using Application.Services;
using AutoMapper;
using Infrastructure.Repositories.Interfaces;
using Domain;
using Domain.Entities;
using Bogus;
using Application.Mappings;
using FluentAssertions;
using Moq;
using Microsoft.Extensions.Logging;

namespace ApplicationUnitTests.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private User _user;

        public UserServiceTests()
        {
            var faker = new Faker();

            _user = new User()
            {
                ID = 1,
                Name = faker.Name.FirstName()
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.ReadById(It.IsAny<int>())).ReturnsAsync(_user);
            _userRepository = userRepositoryMock.Object;

            var reviewRepositoryMock = new Mock<IReviewRepository>();
            reviewRepositoryMock.Setup(x => x.ReadById(It.IsAny<int>()));
            _reviewRepository = reviewRepositoryMock.Object;

            var mappingConfig = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = mappingConfig.CreateMapper();

            var loggerMock = new Mock<ILogger<UserService>>();

            _userService = new UserService(_userRepository,_mapper,_reviewRepository,)
        }

        [Fact]
        public void ShoudBeAvailableToCreate()
        {
            _userService.Should().NotBeNull();
        }
    }
}
