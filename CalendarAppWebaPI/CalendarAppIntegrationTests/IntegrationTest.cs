using CalendarAppWebaPI;
using CalendarAppWebaPI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CalendarAppWebaPI.Contracts;
using Newtonsoft.Json;
using System.Text;
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

        protected async Task AuthenticateAsync(string role,int id)
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync(role,id));
        }

        protected async Task<User> CreateUserAsync(User request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Users.PostUsers, request);
            return await response.Content.ReadAsAsync<User>();
        }

        protected async Task<Event> CreateEventAsync(Event request)
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Events.PostEvents, request);
            return await response.Content.ReadAsAsync<Event>();
        }

        private async Task<string> GetJwtAsync(string role,int id)
        {
            var user = new User
            {
                UserId = id,
                Name = "TestName",
                LastName = "TestLastName",
                Email = "test@integration.com",
                Password = "Password123.",
                Role = role
            };
            if (user.Role == "Admin")
                user.UserId = 300;

          
            string contents = JsonConvert.SerializeObject(user);
            var response = await TestClient.PostAsync(ApiRoutes.Auth.Register, new StringContent(contents, Encoding.UTF8, "application/json"));
            if(response.StatusCode== System.Net.HttpStatusCode.BadRequest)
            {
                UserLoginRequest userReq = new UserLoginRequest(user.Email, user.Password);
                contents = JsonConvert.SerializeObject(user);
                response = await TestClient.PostAsync(ApiRoutes.Auth.Login, new StringContent(contents, Encoding.UTF8, "application/json"));
            }

            var registrationResponseStr = await response.Content.ReadAsStringAsync();
            var registrationResponse = JsonConvert.DeserializeObject<UserWithToken>(registrationResponseStr);
            return registrationResponse.Token;

        }
    }
}
