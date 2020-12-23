using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarAppWebaPI.Models
{
    public class EventRequestSender : IValidatableObject
    {
        [Key]
        [Required]
        public int EventRequestSenderId { get; set; }
        [Required]
        public int EventId { get; set; }

        [Required]
        public int UserId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var _contextOption = new DbContextOptionsBuilder<ApplicationDbContext>().Options;
            ApplicationDbContext _context = new ApplicationDbContext(_contextOption);

            var user = _context.Users.Where(u => u.UserId == UserId).FirstOrDefault();
            if (user == null)
                yield return new ValidationResult("Invalid UserId. No user " + UserId + " in database");

            var events = _context.Events.Where(e => e.EventId == EventId).FirstOrDefault();
            if (events == null)
                yield return new ValidationResult("Invalid EventId. No event " + EventId + " in database");
        }
    }
}