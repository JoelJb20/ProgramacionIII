using Application.DTO;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesByScreensController : ControllerBase
    {

        private readonly IMovieScreenService _screenService;
  
        public MoviesByScreensController(IMovieScreenService cinemaServ)
        {
            _screenService = cinemaServ;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseManager<MoviesByScreensView>>> GetAllMoviesInScreen()
        {
            var response =  await _screenService.GetAllMoviesInScreen();
            return Ok(response);
        }

       
        [HttpGet("ByCinemaId")]
        public async Task<ActionResult<ResponseManager<MoviesByScreensView>>> GetMoviesByCinemaId(int cinemaId)
        {
            var response = await _screenService.GetMoviesByCinemaId(cinemaId);
            return Ok(response);
        }

        [HttpGet("ByScreenId")]
        public async Task<ActionResult<ResponseManager<MoviesByScreensView>>> GetMoviesByScreenId(int screenId)
        {
            var response = await _screenService.GetMoviesByScreenId(screenId);
            return Ok(response);
        }

        [HttpGet("ByMovieId")]
        public async Task<ActionResult<ResponseManager<MoviesByScreensView>>> GetMoviesByMovieId(int movieId)
        {
            var response = await _screenService.GetMoviesByMovieId(movieId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseManager<CinemaScreenDTO>>> UpdateMovieByScreen(MoviesByScreenDTO model)
        {
            var response = await _screenService.UpdateMovieByScreen(model);
            return this.Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseManager<CinemaScreenDTO>>> InsertMovieByScreen(MoviesByScreenDTO model)
        {
            var response = await _screenService.InsertMovieByScreen(model);
            return this.Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseManager>> DeleteMovieInScreen(int movieByScreenId, int userId)
        {
            var response = await _screenService.DeleteMovieInScreen(movieByScreenId, userId);
            return Ok(response);
        }



    }
}
