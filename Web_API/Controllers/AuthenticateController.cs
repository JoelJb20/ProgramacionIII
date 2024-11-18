using Application.DTO;
using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _service;
        public AuthenticateController(IAuthenticateService r )
        {
            _service = r;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ResponseManager<TokenDTO>>> DoLogin(LoginDTO model)
        {
            var response = await _service.DoLogin(model);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("signUp")]
        public async Task<ActionResult<ResponseManager>> DoSignUp(SignUpDTO model)
        {
            var response = await _service.DoSignUp(model);
            return Ok(response);
        }

    }
}
