﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CalendarAppWebaPI.Models
{
    public class Event : IValidatableObject
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
        public DateTime? TimeEnd { get; set; }

        [Required]
        public bool IsFullDay {get;set;}

        [Required]
        public string ThemeColor { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public bool IsPublic { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((TimeEnd - TimeStart > TimeSpan.FromDays(1)) && IsFullDay == true)
                yield return new ValidationResult("Event Can not be fullday-event, Event takes more than one day");

            if (IsFullDay == false && TimeEnd == null)
                yield return new ValidationResult("Event is not a fullday-event. Event has to have TimeEnd value not equal to null");
        }
    }
}