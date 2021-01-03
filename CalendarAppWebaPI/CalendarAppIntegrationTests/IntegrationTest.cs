using CalendarAppWebaPI;
using CalendarAppWebaPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalendarAppWebaPI.Contracts;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Text;
using System;
using Xunit;

namespace CalendarAppIntegrationTests
{

    public class IntegrationTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected readonly HttpClient TestClient;
        private readonly CustomWebApplicationFactory<Startup>
        _factory;
        protected IntegrationTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;

            TestClient = factory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<User> CreateUserAsync(User request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.PostUsers, request);
            return await response.Content.ReadAsAsync<User>();
        }

        private async Task<string> GetJwtAsync()
        {
            var user = new User
            {
                UserId = 200,
                Name = "TestName",
                LastName = "TestLastName",
                Email = "test@integration.com",
                Password = "Password123.",
                Role = "User"
            };

          
            string contents = JsonConvert.SerializeObject(user);
            var response = await TestClient.PostAsync(ApiRoutes.Auth.Register, new StringContent(contents, Encoding.UTF8, "application/json"));
            if(response.StatusCode== System.Net.HttpStatusCode.BadRequest)
            {
                response = await TestClient.GetAsync(ApiRoutes.Auth.Login.Replace("{email}", "test@integration.com").Replace("{password}","Password123."));
            }

            var registrationResponseStr = await response.Content.ReadAsStringAsync();
            var registrationResponse = JsonConvert.DeserializeObject<UserWithToken>(registrationResponseStr);
            return registrationResponse.Token;

        }
    }
}
