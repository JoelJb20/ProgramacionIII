using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieClassificationController : ControllerBase
    {
        private readonly IMovieClassificationService _service;

        public MovieClassificationController(IMovieClassificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseManager<MovieClassificationsView>>> GetAll()
        {
            var response = await _service.GetAll();
            return Ok(response);
        }
    }
}
