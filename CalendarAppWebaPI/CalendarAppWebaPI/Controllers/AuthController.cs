using CalendarAppWebaPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CalendarAppWebaPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JWTSettings _jwtsettings;

        public AuthController(ApplicationDbContext context, IOptions<JWTSettings> jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
        }

        // GET: api/Users/Login
        [HttpGet("Login/{email}/{password}")]
        public ActionResult<UserWithToken> Login(string email, string password)
        {
            var user = _context.Users.Where(u => u.Email == email
                                            && u.Password == password)
                                     .FirstOrDefault();
            UserWithToken userWithToken = new UserWithToken(user);

            if (userWithToken == null)
            {
                return NotFound();
            }

            // signing token here 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token = tokenHandler.WriteToken(token);

            return userWithToken;
        }

        // GET: api/Users/Register
        [HttpPost("Register")]
        public ActionResult<UserWithToken> Register(User user)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ExistingUserByEmail = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (ExistingUserByEmail != null)
            {
                return BadRequest("This Email is already in use");
            }

            _context.Users.Add(user);
            _context.SaveChanges();

            var newUser = _context.Users.Where(u => u.Email == user.Email
                                            && u.Password == user.Password).FirstOrDefault();
            UserWithToken userWithToken = new UserWithToken(newUser);

            if (userWithToken == null)
            {
                return NotFound();
            }

            // signing token here 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, newUser.Email),
                    new Claim(ClaimTypes.Role, newUser.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            userWithToken.Token = tokenHandler.WriteToken(token);

            return userWithToken;
        }
    }
}
