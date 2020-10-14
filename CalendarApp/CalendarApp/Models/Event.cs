﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarApp.Models
{
    public class Event
    {
        [Key]
        [Required]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string Subject { get; set; }

        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }

        [Required]
        public bool IsFullDay {get;set;}

        [Required]
        public string ThemeColor { get; set; }
    }
}