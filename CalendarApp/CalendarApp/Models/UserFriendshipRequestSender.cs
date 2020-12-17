using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarApp.Models
{
    public class UserFriendshipRequestSender
    {
        [Key]
        [Required]
        public int UserFriendshipRequestSenderId { get; set; }
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Person2Id { get; set; }
    }
}