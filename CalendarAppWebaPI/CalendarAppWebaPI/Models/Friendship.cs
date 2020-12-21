using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarAppWebaPI.Models
{
    public class Friendship : IValidatableObject
    {
        [Required]
        public int FriendshipId { get; set; }

        [Required]
        public int Person1Id { get; set; }

        [Required]
        public int Person2Id { get; set; }

        [Required]
        public bool isBlocked { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ApplicationDbContext _context = new ApplicationDbContext(null);
            var user = _context.Users.Where(u => u.UserId == Person1Id).FirstOrDefault();

            if (user == null)
                yield return new ValidationResult("Invalid UserId.No user " + Person1Id + " in database");

            user = _context.Users.Where(u => u.UserId == Person2Id).FirstOrDefault();
            if (user == null)
                yield return new ValidationResult("Invalid UserId.No user " + Person2Id + " in database");
        }
    }
}