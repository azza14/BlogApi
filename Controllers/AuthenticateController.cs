using BlogApi.DTO;
using BlogApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogApi.Repositories;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private IGenericRepository<User> _repoUser;
        private readonly ApplicationSettings _appSettings;
        public AuthenticateController(IGenericRepository<User> repoUser)
        {
            _repoUser = repoUser;
        }
        [HttpPost]
        [Route("Register")]
        //POST : /api/ApplicationUser/Register
        public  IActionResult Register(RegisterViewModel model)
        {
            var userExists = _repoUser.FindByCondition(x => x.UserName == model.UserName).FirstOrDefault();
            if (userExists!=null )
                return StatusCode(StatusCodes.Status500InternalServerError,  
                    new  { Status = "Error", Message = "User already exists!" });

            var user = new User()
            {
                Email = model.Email,
                Password= model.Password,
                UserName=model.UserName,
                Role= model.Role
               // SecurityStamp = Guid.NewGuid().ToString(),
            };
            try
            {
                _repoUser.Insert(user);
                _repoUser.Save();
                return Ok(new { Status = "Success", Message = "User created successfully!" });

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  new
                  {
                      Status = "Error",
                      Message = "User creation failed! Please check user details and try again."
                  });
            }
          

        }

        [HttpPost]
        [Route("Login")]
        //POST : /api/ApplicationUser/Login
        public IActionResult Login(LoginModel model)
        {
            var user =  _repoUser.FindByCondition(x => x.Email  == model.Email).FirstOrDefault();
            if (user != null)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }

    }
}
