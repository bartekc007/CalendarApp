using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarAppWebaPI.Models
{
    public class Friendship
    {
        [Required]
        public int FriendshipId { get; set; }

        [Required]
        public int Person1Id { get; set; }

        [Required]
        public int Person2Id { get; set; }

        [Required]
        public bool isBlocked { get; set; }
    }
}