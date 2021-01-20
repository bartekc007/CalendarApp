using CalendarAppWebaPI;
using CalendarAppWebaPI.Contracts;
using CalendarAppWebaPI.DTO;
using CalendarAppWebaPI.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
            await AuthenticateAsync("User",200, "test3@integration.com");

            // Act
            var createdUser = await CreateUserAsync(new User
            {
                UserId = 201,
                Name = "TestName",
                LastName = "TestLastName",
                Email = "test3test1@integration.com",
                Password = "Password123.",
                Role = "User"
            });
            var response = await TestClient.GetAsync(ApiRoutes.Users.GetUsers);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<UserDTO>>()).Should().NotBeNull();
        }

        [Fact]
        public async Task Get_User_WithID_Test()
        {
            // Arange
            await AuthenticateAsync("User",202, "test4@integration.com");
            var createdUser = await CreateUserAsync(new User
            {
                UserId = 203,
                Name = "TestName",
                LastName = "TestLastName",
                Email = "test7@integration.com",
                Password = "Password123.",
                Role = "User"
            });
        

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Users.GetUser.Replace("{userId}",createdUser.UserId.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedUser = await response.Content.ReadAsAsync<User>();
            returnedUser.UserId.Should().Be(createdUser.UserId);
        }

        [Fact]
        public async Task Put_User_WithValidData_Test()
        {
            // Arange
            await AuthenticateAsync("User",204, "test5@integration.com");
            var createdUser = await CreateUserAsync(new User
            {
                UserId = 205,
                Name = "TestName",
                LastName = "TestLastName",
                Email = "test8@integration.com",
                Password = "Password123.",
                Role = "User"
            });

            // Act
            var user = new User
            {
                UserId = createdUser.UserId,
                Name = "ChangedTestName",
                LastName = "ChangedTestLastName",
                Email = "test9@integration.com",
                Password = "Password123.",
                Role = "User"
            };

            string contents = JsonConvert.SerializeObject(user);
            var response = await TestClient.PutAsync(ApiRoutes.Users.PutUsers
                .Replace("{userId}", createdUser.UserId.ToString()),new StringContent(contents, Encoding.UTF8, "application/json"));

            var getResponse = await TestClient.GetAsync(ApiRoutes.Users.GetUser.Replace("{userId}", createdUser.UserId.ToString()));
            
            // Assert
            var returnedUser = await getResponse.Content.ReadAsAsync<User>();
            returnedUser.UserId.Should().Be(user.UserId);
            returnedUser.Name.Should().Be(user.Name);
            returnedUser.LastName.Should().Be(user.LastName);
        }
    }
}
