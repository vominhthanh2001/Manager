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
    [Route("api/list-user-manager")]
    public class ListUserManagerController : ControllerBase
    {
        private readonly IListUserManager _service;
        public ListUserManagerController(IListUserManager service)
        {
            _service = service;
        }

        [HttpPost("add")]
        public async Task<ActionResult<ListUserResponseModel>> AddAsync(UserModel? user)
        {
            var resAdd = await _service.AddAsync(user);
            return Ok(resAdd);
        }

        [HttpPost("add-list")]
        public async Task<ActionResult<ListUserResponseModel>> AddAsync([FromBody] List<UserModel?>? users)
        {
            var resAdd = await _service.AddAsync(users);
            return Ok(resAdd);
        }

        [HttpGet("delete/{id}")]
        public async Task<ActionResult<ListUserResponseModel>> DeleteAsync(int? id)
        {
            var resDelete = await _service.DeleteAsync(id);
            return Ok(resDelete);
        }

        [HttpPost("delete-list")]
        public async Task<ActionResult<ListUserResponseModel>> DeleteAsync([FromBody] List<int?>? ids)
        {
            var resDelete = await _service.DeleteAsync(ids);
            return Ok(resDelete);
        }

        [HttpGet("find/{id}")]
        public async Task<ActionResult<UserModel?>> FindAsync(int? id)
        {
            var resFind = await _service.FindAsync(id);
            return Ok(resFind);
        }

        [HttpGet("get-all")]
        public async Task<ActionResult<List<UserModel?>?>> GetAllAsync()
        {
            var resGetAll = await _service.GetAllAsync();
            return Ok(resGetAll);
        }

        [HttpPost("update")]
        public async Task<ActionResult<ListUserResponseModel>> UpdateAsync(UserModel? user)
        {
            var resUpdate = await _service.UpdateAsync(user);
            return Ok(resUpdate);
        }

        [HttpPost("update-list")]
        public async Task<ActionResult<ListUserResponseModel>> UpdateAsync([FromBody] List<UserModel?> users)
        {
            var resUpdate = await _service.UpdateAsync(users);
            return Ok(resUpdate);
        }
    }
}
