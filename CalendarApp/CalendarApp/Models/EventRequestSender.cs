using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarApp.Models
{
    public class EventRequestSender
    {
        [Required]
        public int EventId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}