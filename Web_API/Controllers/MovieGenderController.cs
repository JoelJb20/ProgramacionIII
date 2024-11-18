using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieGenderController : ControllerBase
    {
        private readonly IMovieGenderService _service;

        public MovieGenderController(IMovieGenderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseManager<MovieGendersView>>> GetAll()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }
    }
}
