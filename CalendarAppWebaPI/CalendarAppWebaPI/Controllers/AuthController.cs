using CalendarAppWebaPI.DTO;
using CalendarAppWebaPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        // POST: api/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(UserLoginRequest userRequest)
        {
            var user = _context.Users.Where(u => u.Email == userRequest.Email).FirstOrDefault();
            if(user==null)
            {
                return NotFound();
            }

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(userRequest.Password, user.Password);

            if (!isValidPassword)
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,principal, props).Wait();
            UserDTO userDTO = new UserDTO(user);
            return userDTO;
            /*UserWithToken userWithToken = new UserWithToken(user);

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

            return userWithToken;*/
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(User user)
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

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            var newUser = _context.Users.Where(u => u.Email == user.Email
                                            && u.Password == user.Password).FirstOrDefault();
            UserDTO userDTO = new UserDTO(newUser);


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var identity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props = new AuthenticationProperties();
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

            return userDTO;

            // signing token here 
            /*var tokenHandler = new JwtSecurityTokenHandler();
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
            userWithToken.Token = tokenHandler.WriteToken(token);*/

        }

        // POST: /api/Auth/Logout
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}
