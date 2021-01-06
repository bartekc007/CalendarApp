using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarAppWebaPI.Models
{
    public class UserFriendshipRequestSender
    {
        [Key]
        [Required]
        public int UserFriendshipRequestSenderId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int Person2Id { get; set; }
    }
}