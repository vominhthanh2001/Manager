using Manager.Api.Data;
using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers.Users
{
    [Route("api/user-manager")]
    [ApiController]
    public class UserManagerController : ControllerBase
    {
        private readonly IUserManager _service;
        public UserManagerController(IUserManager service)
        {
            _service = service;
        }

        //[AllowAnonymous]
        [HttpGet("forgot-password/{username}")]
        public async Task<ActionResult<UserModel>> ForgotPassword(string username)
        {
            var resForgotPassword = await _service.ForgotPassword(username);
            return Ok(resForgotPassword);
        }

        //[AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserResponseModel>> Login([FromBody] UserModel user)
        {
            var resLogin = await _service.Login(user);
            if (!resLogin.Status)
                return Unauthorized(resLogin);

            return Ok(resLogin);
        }

        //[Authorize(Roles = "User,Administration")]
        [HttpPost("logout")]
        public async Task<ActionResult<UserResponseModel>> Logout([FromBody] UserModel user)
        {
            var resLogout = await _service.Logout(user);
            return Ok(resLogout);
        }

        //[AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserResponseModel>> Register([FromBody] UserModel user)
        {
            var resRegister = await _service.Register(user);
            return Ok(resRegister);
        }
    }
}
