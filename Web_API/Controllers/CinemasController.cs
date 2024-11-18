using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemasController : ControllerBase
    {

        private readonly ICinemaService _cinemaService;
  
        public CinemasController(ICinemaService cinemaServ)
        {
           _cinemaService = cinemaServ;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseManager<CinemasView>>> Cinemas()
        {
            var response =  await _cinemaService.GetAllCinemas();
            return Ok(response);
        }

       
        [HttpGet("ByName")]
        public async Task<ActionResult<ResponseManager<CinemasView>>> GetByName(string cinemaName)
        {
            var response = await _cinemaService.GetCinemasByName(cinemaName);
            return Ok(response);
        }

        [HttpGet("ById")]
        public async Task<ActionResult<ResponseManager<CinemasView>>> GetByID(int cinemaId)
        {
            var response = await _cinemaService.GetById(cinemaId);
            return Ok(response);
        }

        [HttpGet("WithMoviesAssignedByMovieId")]
        public async Task<ActionResult<ResponseManager<CinemasView>>> GetCinemasWithMoviesAssignedByMovieId(int movieId)
        {
            var response = await _cinemaService.GetCinemasWithMoviesAssignedByMovieId(movieId);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseManager>> Delete(int cinemaId, int userId)
        {
            var response = await _cinemaService.Delete(cinemaId, userId);
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<ActionResult<ResponseManager<CinemaDTO>>> Update(CinemaDTO cine)
        {
            var response = await _cinemaService.Update(cine);
            return this.Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseManager<CinemaDTO>>> Create(CinemaDTO cine)
        {
            var response = await _cinemaService.Create(cine);
            return this.Ok(response);
        }
   
       

    }
}
