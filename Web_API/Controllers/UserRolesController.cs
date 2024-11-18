using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRoleService;

        public UserRolesController(IUserRolesService service)
        {
            _userRoleService = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseManager<UserRolesView>>> GetAll()
        {
            var response = await _userRoleService.GetAll();
            return Ok(response);
        }

        [HttpGet("ById")]
        public async Task<ActionResult<ResponseManager<UserRolesView>>> GetById(int roleId)
        {
            var response = await _userRoleService.GetById(roleId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseManager<UserRoleDTO>>> InsertRole(UserRoleDTO model)
        {
            var response = await _userRoleService.Insert(model);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseManager<UserRoleDTO>>> UpdateRole(UserRoleDTO model)
        {
            var response = await _userRoleService.Update(model);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseManager>> Delete(int roleId, int userId)
        {
            var response = await _userRoleService.Delete(roleId, userId);
            return Ok(response);
        }
    }
}
