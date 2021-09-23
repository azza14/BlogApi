using Microsoft.AspNetCore.Mvc;
using BlogApi.Repositories;
using System.Linq;
using BlogApi.Entities;
using BlogApi.DTO;

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IGenericRepository<User> _repoUser;

        public AccountsController(IGenericRepository<User> repository)
        {
            _repoUser = repository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _repoUser.GetAll();
            return Ok(userList);
        }
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value" + id;
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterViewModel model)
        {
            try
            {
                var newUser = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password= model.Password,
                    Role= model.Role
                };
                _repoUser.Insert(newUser);
                _repoUser.Save();
                return Ok(new { message = "User Register successfully" });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            var user = _repoUser
                .GetByCondition(u => u.Email == model.Email && u.Password == model.Password)
                .FirstOrDefault();
            if (user != null)
            {
                return Ok(new { message = "User login successfully" });
            }

            else
            {
               return BadRequest(new { message = "Username or password is incorrect." });
            }
        }

        [HttpPost]       
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var user = _repoUser.GetByCondition(u => u.Password == model.CurrentPassword).FirstOrDefault();
            if (user != null)
            {
                user.Password = model.NewPassword;
                _repoUser.Update(user);
                _repoUser.Save();
            }
            
            return Ok( new { message="update password succefull"});
        }

        [HttpPost]
        [Route("RestPassword")]
        public IActionResult RestPassword(string email)
        {
            var user = _repoUser.GetByCondition(u => u.Email == email).FirstOrDefault();
            
            if (user != null)
            {

               // user.Password = model.NewPassword;
                _repoUser.Update(user);
                _repoUser.Save();
                return Ok();
            }
            return Ok();
        }
    }
}
            
