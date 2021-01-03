using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.Models
{
    public class UserWithToken : IValidatableObject
    {
        public UserWithToken()
        {

        }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.Password = user.Password;
        }

        [Required]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string Token { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Password validation
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var isValidated = hasNumber.IsMatch(Password) && hasUpperChar.IsMatch(Password) && hasMinimum8Chars.IsMatch(Password);
            if (!isValidated)
                yield return new ValidationResult("Invalid Password");

            // Email address validation
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(Email);
            if (!match.Success)
                yield return new ValidationResult("Invalid Email Address");

            // Name and Lastname validation
            regex = new Regex(@"^[a-zA-Z]+$");
            match = regex.Match(Name);
            if (!match.Success)
                yield return new ValidationResult("Invalid Name, Can contain only alphabets characters");

            match = regex.Match(LastName);
            if (!match.Success)
                yield return new ValidationResult("Invalid Lastname, Can contain only alphabets characters");
        }
    }
}
