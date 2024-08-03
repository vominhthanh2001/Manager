using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers.Users
{
    [ApiController]
    [Authorize(Roles = "Administration")]
    [Route("api/tool-product")]
    public class ToolProductManagerController : ControllerBase
    {
        private readonly IToolProductManager _service;
        public ToolProductManagerController(IToolProductManager service)
        {
            _service = service;
        }

        [HttpGet("create/{name}")]
        public async Task<ActionResult<bool>> CreateAsync(string? name)
        {
            var resCreateAsync = await _service.CreateAsync(name);
            return Ok(resCreateAsync);
        }

        [HttpGet("delete/{id}")]
        public async Task<ActionResult<bool>> DeleteAsync(int? id)
        {
            var resCreateAsync = await _service.DeleteAsync(id);
            return Ok(resCreateAsync);
        }

        [HttpGet("find/{id}")]
        public async Task<ActionResult<ToolProductModel?>> FindAsync(int? id)
        {
            var resFindAsync = await _service.FindAsync(id);
            return Ok(resFindAsync);
        }

        [HttpGet("find/get-all")]
        public async Task<ActionResult<List<ToolProductModel>?>> GetAllAsync()
        {
            var resGetAllAsync = await _service.GetAllAsync();
            return Ok(resGetAllAsync);
        }
    }
}
