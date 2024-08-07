using Manager.Shared.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Manager.Api.Controllers.Users
{
    [Route("api/ip-connect")]
    [ApiController]
    public class IpAdressController : ControllerBase
    {
        private readonly IIpAdress _service;
        public IpAdressController(IIpAdress service)
        {
            _service = service;
        }

        [HttpGet("get-ip")]
        public async Task<ActionResult<string>> GetIpConnect(HttpRequest request)
        {
         string ip = await _service.GetIpConnect(request);
            return Ok(ip);
        }
    }
}
