using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CalendarAppWebaPI.Models
{
    public class EventMembers
    {
        [Key]
        [Required]
        public int EventMembersId { get; set; }
        [Required]
        public int UserID { get; set; }

        [Required]
        public int EventID { get; set; }
    }
}