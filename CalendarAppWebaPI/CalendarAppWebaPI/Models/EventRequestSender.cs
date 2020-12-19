using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarAppWebaPI.Models
{
    public class EventRequestSender
    {
        [Key]
        [Required]
        public int EventRequestSenderId { get; set; }
        [Required]
        public int EventId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}