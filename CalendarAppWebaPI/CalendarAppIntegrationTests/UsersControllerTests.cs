using CalendarAppWebaPI;
using CalendarAppWebaPI.Contracts;
using CalendarAppWebaPI.Models;
using FluentAssertions;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CalendarAppIntegrationTests
{
    public class UsersControllerTests : IntegrationTest
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public UsersControllerTests(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_Users_Test()
        {
            // Arange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Users.GetUsers);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<User>>()).Should().NotBeNull();
        }

        [Fact]
        public async Task Get_User_WithID_Test()
        {
            // Arange
            await AuthenticateAsync();
            var createdUser = await CreateUserAsync(new User
            {
                UserId = 201,
                Name = "TestName",
                LastName = "TestLastName",
                Email = "test@integration.com",
                Password = "Password123.",
                Role = "User"
            });
        

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Users.GetUser.Replace("{userId}",createdUser.UserId.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedUser = await response.Content.ReadAsAsync<User>();
            returnedUser.UserId.Should().Be(createdUser.UserId);
            returnedUser.Email.Should().Be(createdUser.Email);
        }
    }
}
