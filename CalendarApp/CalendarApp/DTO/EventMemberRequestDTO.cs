using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.DTO
{
    public class EventMemberRequestDTO
    {
        public int EventMemberRequestId { get; set; }
        public int EventID { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool IsAccepted { get; set; }
    }
}