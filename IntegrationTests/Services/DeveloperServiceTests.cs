using Application.Services;
using ApplicationIntegrationTests;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;
using Application.Requests;
using Bogus;
using Application.Exceptions;
using Application.Requests.DeveloperRequests;

namespace ApplicationUnitTests.Services
{
    public class DeveloperServiceTests : IClassFixture<TestingFixture>
    {
        private readonly TestingFixture _fixture;
        private readonly IDeveloperService _developerService;

        public DeveloperServiceTests(TestingFixture fixture)
        {
            _fixture = fixture;
            _developerService = fixture.ServiceProvider.GetRequiredService<IDeveloperService>();
        }

        [Fact]
        public async Task Create_ShouldCreateDeveloper_AndReturnId()
        {
            var request = new CreateDeveloperRequest
            {
                Name = "Studio",
                Description = "Maybe good maybe bad i don't know",
                LogoURL = "LOGoLOGoLOGoLOGoLOGo"
            };

            var developerId = await _developerService.Create(request);
            developerId.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ReadBy_ShouldReturnDeveloper_WhenExists()
        {
            var developer = await _fixture.CreateDeveloper();
            var result = await _developerService.ReadById(developer.ID);

            result.Should().NotBeNull();
            result.ID.Should().Be(developer.ID);
        }

        [Fact]
        public async Task ReadAll_ShouldReturnAllDevelopers()
        {
            await _fixture.CreateDeveloper();
            await _fixture.CreateDeveloper();

            var result = await _developerService.ReadAll();
            result.Count().Should().BeGreaterThanOrEqualTo(2);
        }

        [Fact]
        public async Task Update_ShouldModifyDeveloper()
        {
            var developer = await _fixture.CreateDeveloper();
            var request = new UpdateDeveloperRequest
            {
                ID = developer.ID,
                Name = "Updated Developer Name",
                Description = "Updated Description",
                LogoURL = "updated-logo.png"
            };

            await _developerService.Update(request);
            var result = await _developerService.ReadById(request.ID);

            result.ID.Should().Be(request.ID);
            result.Name.Should().Be(request.Name);
            result.Description.Should().Be(request.Description);
            result.LogoURL.Should().Be(request.LogoURL);
        }
    }
}
