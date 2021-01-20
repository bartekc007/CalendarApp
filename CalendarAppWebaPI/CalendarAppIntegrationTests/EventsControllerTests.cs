using CalendarAppWebaPI;
using CalendarAppWebaPI.Contracts;
using CalendarAppWebaPI.Models;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CalendarAppIntegrationTests
{
    public class EventsControllerTests : IntegrationTest
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        public EventsControllerTests(CustomWebApplicationFactory<Startup> factory) :base(factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAll_Events_Test()
        {
            // Arange
            await AuthenticateAsync("Admin",250, "test0@integration.com");
            var createdEvent = await CreateEventAsync(new Event
            {
                EventId = 100,
                Subject = "TestSubject",
                Description = "TestDescription",
                TimeStart = new DateTime(2020, 1, 1, 12, 0, 0),
                TimeEnd = new DateTime(2020,1,4,12,0,0),
                IsFullDay = false,
                ThemeColor = "blue",
                UserID = 250,
                IsPublic = true
            });

            // Act
            var eventResponse = await TestClient.GetAsync(ApiRoutes.Events.GetEvents);

            // Assert
            eventResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            (await eventResponse.Content.ReadAsAsync<List<Event>>()).Should().NotBeNull();
        }


        [Fact]
        public async Task Get_Event_WithID_Test()
        {
            // Arange
            await AuthenticateAsync("Admin",251, "test1@integration.com");
            var createdEvent = await CreateEventAsync(new Event
            {
                EventId = 102,
                Subject = "TestSubject",
                Description = "TestDescription",
                TimeStart = new DateTime(2020, 1, 1, 12, 0, 0),
                TimeEnd = new DateTime(2020, 1, 4, 12, 0, 0),
                IsFullDay = false,
                ThemeColor = "blue",
                UserID = 251,
                IsPublic = true
            });

            // Act
            var response = await TestClient.GetAsync(ApiRoutes.Events.GetEvent.Replace("{id}",createdEvent.EventId.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedEvent = await response.Content.ReadAsAsync<Event>();
            returnedEvent.EventId.Should().Be(createdEvent.EventId);
            returnedEvent.UserID.Should().Be(createdEvent.UserID);
        }

        [Fact]
        public async Task Put_Event_WithValidData_Test()
        {
            // Arange
            await AuthenticateAsync("Admin", 252, "test2@integration.com");
            var createdEvent = await CreateEventAsync(new Event
            {
                EventId = 103,
                Subject = "TestSubject",
                Description = "TestDescription",
                TimeStart = new DateTime(2020, 1, 1, 12, 0, 0),
                TimeEnd = new DateTime(2020, 1, 4, 12, 0, 0),
                IsFullDay = false,
                ThemeColor = "blue",
                UserID = 252,
                IsPublic = true
            });

            // Act
            var editedEvent = new Event
            {
                EventId = 103,
                Subject = "ChangedTestSubject",
                Description = "ChangedTestDescription",
                TimeStart = new DateTime(2020, 1, 1, 12, 0, 0),
                TimeEnd = new DateTime(2020, 1, 4, 12, 0, 0),
                IsFullDay = false,
                ThemeColor = "blue",
                UserID = 252,
                IsPublic = true
            };
            string contents = JsonConvert.SerializeObject(editedEvent);
            var response = await TestClient.PutAsync(ApiRoutes.Events.PutEvents
                .Replace("{id}", createdEvent.EventId.ToString()), new StringContent(contents, Encoding.UTF8, "application/json"));
            var getResponse = await TestClient.GetAsync(ApiRoutes.Events.GetEvent.Replace("{id}", createdEvent.EventId.ToString()));

            // Assert
            var returnedEvent = await getResponse.Content.ReadAsAsync<Event>();
            returnedEvent.EventId.Should().Be(editedEvent.EventId);
            returnedEvent.UserID.Should().Be(editedEvent.UserID);
            returnedEvent.Description.Should().Be(editedEvent.Description);
            returnedEvent.Subject.Should().Be(editedEvent.Subject);
        }
    }
}
