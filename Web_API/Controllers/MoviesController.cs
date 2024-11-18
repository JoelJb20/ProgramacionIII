using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web_API.Utilities;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IMovieService _movieService;
  
        public MoviesController(IMovieService movieServ)
        {
            this._movieService = movieServ;
        }
     
        [HttpGet]
        public async Task<ActionResult<ResponseManager<MoviesView>>> GetAllMovies()
        {
            var response = await _movieService.GetAllMovies();
            return Ok(response);
        }

        [HttpGet("GetAllAssigned")]
        public async Task<ActionResult<ResponseManager<MoviesView>>> GetAllMoviesAssigned()
        {
            var response = await _movieService.GetAllMoviesAssigned();
            return Ok(response);
        }

        [HttpGet("ByName")]
        public async Task<ActionResult<ResponseManager<MoviesView>>> GetMoviesByName(string movieName)
        {
            var response = await _movieService.GetMoviesByName(movieName);
            return Ok(response);
        }

        [HttpGet("ById")]
        public async Task<ActionResult<ResponseManager<MoviesView>>> GetById(int movieId)
        {
            var response = await _movieService.GetById(movieId);
            return Ok(response);
        }

        [HttpGet("ActorsInMovie")]
        public async Task<ActionResult<ResponseManager<ActorsInMoviesView>>> GetActorsByMovie(int movieId)
        {
            var response = await _movieService.GetActorsByMovie(movieId);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseManager>> Delete(int movieId, int userId)
        {
            var response = await _movieService.Delete(movieId, userId);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseManager<MovieDTO>>> Update( MovieDTO model)
        {
            var response = await _movieService.Update(model);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseManager<MovieDTO>>> Create( MovieDTO model)
        {
            var response = await _movieService.Create(model);
            return Ok(response);
        }

        [HttpPut("UploadImage")]
        public async Task<ActionResult<ResponseManager>> UploadImage([FromForm] MovieImageDTO model)
        {
            var response = await _movieService.UploadImage(model);
            return Ok(response);
        }

        [HttpDelete("DeleteImage")]
        public async Task<ActionResult<ResponseManager>> DeleteImage(int movieId, int userId)
        {
            var response = await _movieService.DeleteImage(movieId, userId);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("ViewImage/{movieId}")]
        public async Task<ActionResult> ViewDocument(int movieId)
        {
            var response = new ResponseManager<MoviesView>();

            response = await _movieService.GetById(movieId);

            if (!response.Succeded)
            {
                return StatusCode(400, response);
            }

            if (response.SingleData is null)
            {
                throw new CustomException($"La pelicula seleccionada no ha podido ser encontrada.", null, HttpStatusCode.BadRequest);
            }

            string base64 = response.SingleData.ImageBytes ?? "";

            if (string.IsNullOrWhiteSpace(base64))
            {
                throw new CustomException($"La pelicula seleccionada no tiene imagen asignada.", null, HttpStatusCode.BadRequest);
            }

            var bytes = Convert.FromBase64String(base64);

            string completeFileName = $"{response.SingleData.ImageName}{response.SingleData.ImageExtension}";
            var mime = ContentTypeMapper.GetMimeType(completeFileName);

            if(mime == null)
            {
                throw new CustomException($"La imagen encontrada no es valida.", null, HttpStatusCode.BadRequest);
            }

            return File(bytes, mime, enableRangeProcessing: true);
        }


        [HttpDelete("DeleteActorInMovie")]
        public async Task<ActionResult<ResponseManager>> DeleteActor(int acInMoId, int userId)
        {
            var response = await _movieService.DeleteActorInMovie(acInMoId, userId);
            return Ok(response);
        }

    }
}
