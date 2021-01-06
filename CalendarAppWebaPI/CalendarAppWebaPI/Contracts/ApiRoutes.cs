using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.Contracts
{
    public class ApiRoutes
    {
        public static class Auth
        {
            public const string Register = "api/Auth/Register";
            public const string Login = "api/Auth/Login";
        }
        public static class Users
        {
            public const string GetUser = "api/Users/{userId}";
            public const string GetUsers = "api/Users";
            public const string PostUsers = "api/Users";
            public const string PutUsers = "api/Users/{userId}";
        }
        public static class Events
        {
            public const string GetEvent = "api/Events/{id}";
            public const string GetEvents = "api/Events";
            public const string PostEvents = "api/Events";
            public const string PutEvents = "api/Events/{id}";
        }
        
    }

    
}
