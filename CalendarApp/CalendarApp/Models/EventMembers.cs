using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Models
{
    public class EventMembers
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        public int EventID { get; set; }

    }
}