using CalendarAppWebaPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.DTO
{
    public class UserDTO
    {
        public UserDTO(User user)
        {
            this.UserID = user.UserId;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.Role = user.Role;
        }

        public UserDTO(){}

        public int UserID { get; private set; }
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Role { get; private set; }
    }
}
