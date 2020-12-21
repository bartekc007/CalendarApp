using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CalendarAppWebaPI.Models
{
    public class UserFriendshipRequestSender : IValidatableObject
    {
        [Key]
        [Required]
        public int UserFriendshipRequestSenderId { get; set; }
        [Required]
        public int UserId { get; set; }

        [Required]
        public int Person2Id { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            ApplicationDbContext _context = new ApplicationDbContext(null);
            var user = _context.Users.Where(u => u.UserId == UserId).FirstOrDefault();

            if (user == null)
                yield return new ValidationResult("Invalid UserId.No user " + UserId + " in database");

            user = _context.Users.Where(u => u.UserId == Person2Id).FirstOrDefault();
            if (user == null)
                yield return new ValidationResult("Invalid UserId.No user " + Person2Id + " in database");
        }
    }
}