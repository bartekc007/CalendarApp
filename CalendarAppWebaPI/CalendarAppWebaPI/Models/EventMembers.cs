using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CalendarAppWebaPI.Models
{
    public class EventMembers : IValidatableObject
    {
        [Key]
        [Required]
        public int EventMembersId { get; set; }
        [Required]
        public int UserID { get; set; }

        [Required]
        public int EventID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ApplicationDbContext _context = new ApplicationDbContext(null);
            var user = _context.Users.Where(u => u.UserId == UserID).FirstOrDefault();
            if (user == null)
                yield return new ValidationResult("Invalid UserId. No user " + UserID + " in database");

            var events = _context.Events.Where(e => e.EventId == EventID).FirstOrDefault();
            if (events == null)
                yield return new ValidationResult("Invalid EventId. No event " + EventID + " in database");
        }
    }
}