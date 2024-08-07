using Manager.Shared.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers.Users
{
    [Route("api/anti-hacker")]
    [ApiController]
    public class AntiHackerController : ControllerBase
    {
        private readonly IAntiHacker _service;

        public AntiHackerController(IAntiHacker service)
        {
            _service = service;
        }

        [HttpGet("execute")]
        public async Task<bool> AntiHacker()
        {
            if (!Request.Headers.TryGetValue("Authorization", out var jwt))
            {
                throw new NotImplementedException();
            }

            jwt = jwt.ToString().Replace("Bearer ", "");
            
            return await _service.AntiHacker(jwt);
        }
    }
}
