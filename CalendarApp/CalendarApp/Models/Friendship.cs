﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarApp.Models
{
    public class Friendship
    {
        [Required]
        public int FriendshipId { get; set; }

        [Required]
        public string Person1Id { get; set; }

        [Required]
        public string Person2Id { get; set; }

        [Required]
        public bool isBlocked { get; set; }
    }
}