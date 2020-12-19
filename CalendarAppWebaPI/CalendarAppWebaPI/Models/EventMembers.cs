using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalendarAppWebaPI.Models
{
    public class EventMembers
    {
        [Key]
        [Required]
        public int EventMembersId { get; set; }
        [Required]
        public string UserID { get; set; }

        [Required]
        public int EventID { get; set; }

    }
}