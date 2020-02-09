using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kiki.Models;
using Kiki.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Kiki.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly KikiContext _dbContext;
        private readonly SignInManager<KikiUser> _signInManager;
        private readonly UserManager<KikiUser>   _userManager;
        private readonly IConfiguration          _configuration;

        public AccountsController(
            KikiContext dbContext,
            UserManager<KikiUser>   userManager,
            SignInManager<KikiUser> signInManager,
            IConfiguration          configuration
        )
        {
            _dbContext = dbContext;
            _userManager   = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("api/auth/login")]
        public async Task<LoginUserInfo> Login([FromBody] LoginDto model)
        {
            //todo: test data population, remove
            if (!_dbContext.Users.Any())
            {
                var user = new KikiUser
                           {
                               UserName = "abc",
                               Email    = "abc@abc.abc",
                               Id = new Guid()
                           };
                
                var testResult =  _userManager.CreateAsync(user, "abc").Result;
                if (!testResult.Succeeded)
                {
                    throw new Exception(testResult.Errors.First().Description);
                }
                
            }
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                if (appUser == null)
                {
                    throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
                }
                string jwt = GenerateJwtToken(model.Username, appUser);
                return new LoginUserInfo(appUser.UserName, appUser.Id, appUser.Email, jwt);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost]
        [Route("api/auth/register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new KikiUser
                       {
                           UserName = model.Username,
                           Email    = model.Email,
                           Id = new Guid()
                       };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return GenerateJwtToken(model.Username, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private string GenerateJwtToken(string username, KikiUser user)
        {
            var claims = new List<Claim>
                         {
                             new Claim(JwtRegisteredClaimNames.Sub, username),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                         };

            var key     = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds   = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                                             _configuration["JwtIssuer"],
                                             _configuration["JwtIssuer"],
                                             claims,
                                             expires: expires,
                                             signingCredentials: creds
                                            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginUserInfo
        {
            public string Username { get; set; }
            public string Email { get; set; }
            public Guid Id { get; set; }
            public string JWT { get; set; }

            public LoginUserInfo(string username, Guid id, string email, string jwt)
            {
                Username = username;
                Id = id;
                Email = email;
                JWT = jwt;
                
            }
        }
        public class LoginDto
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }
        }

        public class RegisterDto
        {
            
            [Required]
            public string Username { get; set; }
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}