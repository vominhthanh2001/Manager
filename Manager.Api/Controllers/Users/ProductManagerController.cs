using Manager.Shared.Contracts;
using Manager.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers.Users
{
    [ApiController]
    //[Authorize(Roles = "Administration")]
    [Route("api/product")]
    public class ProductManagerController : ControllerBase
    {
        private readonly IProductManager _service;
        public ProductManagerController(IProductManager service)
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
        public async Task<ActionResult<ProductModel?>> FindAsync(int? id)
        {
            var resFindAsync = await _service.FindAsync(id);
            return Ok(resFindAsync);
        }

        [HttpGet("find-by-name/{name}")]
        public async Task<ActionResult<ProductModel?>> FindAsync(string? name)
        {
            var resFindAsync = await _service.FindAsync(name);
            return Ok(resFindAsync);
        }

        [HttpPost("update")]
        public async Task<ActionResult<bool>> UpdateAsync([FromBody] ProductModel? product)
        {
            var resFindAsync = await _service.UpdateAsync(product);
            return Ok(resFindAsync);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<ProductModel>?>> GetAllAsync()
        {
            var resGetAllAsync = await _service.GetAllAsync();
            return Ok(resGetAllAsync);
        }
    }
}
