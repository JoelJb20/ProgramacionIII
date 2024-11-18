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
    public class CinemaScreensController : ControllerBase
    {

        private readonly ICinemaScreenService _screenService;
  
        public CinemaScreensController(ICinemaScreenService cinemaServ)
        {
            _screenService = cinemaServ;
        }


        [HttpGet]
        public async Task<ActionResult<ResponseManager<CinemaScreensView>>> GetAllScreens()
        {
            var response =  await _screenService.GetAllScreens();
            return Ok(response);
        }

       
        [HttpGet("ByCinemaId")]
        public async Task<ActionResult<ResponseManager<CinemasView>>> GetScreensByCinemaId(int cinemaId)
        {
            var response = await _screenService.GetScreensByCinemaId(cinemaId);
            return Ok(response);
        }
     
        
        
        [HttpPut]
        public async Task<ActionResult<ResponseManager<CinemaScreenDTO>>> UpdateScreen(CinemaScreenDTO cine)
        {
            var response = await _screenService.UpdateScreen(cine);
            return this.Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ResponseManager<CinemaScreenDTO>>> InsertScreen(CinemaScreenDTO cine)
        {
            var response = await _screenService.InsertScreen(cine);
            return this.Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseManager>> Delete(int screenId, int userId)
        {
            var response = await _screenService.DeleteScreen(screenId, userId);
            return Ok(response);
        }



    }
}
