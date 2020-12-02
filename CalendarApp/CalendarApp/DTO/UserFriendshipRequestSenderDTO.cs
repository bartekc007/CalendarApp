using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.DTO
{
    public class UserFriendshipRequestSenderDTO
    {
        public string UserId { get; set; }

        public string Person2Id { get; set; }
        public string Person2Name { get; set; }

        public bool IsAccepetd { get; set; }
    }
}