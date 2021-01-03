using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.Contracts
{
    public class ApiRoutes
    {
        public static class Users
        {
            public const string GetUser = "api/Users/{userId}";
            public const string GetUsers = "api/Users";
            public const string PostUsers = "api/Users";
        }

        public static class Auth
        {
            public const string Register = "api/Auth/Register";
            public const string Login = "api/Auth/Login/{email}/{password}";
        }
    }

    
}
