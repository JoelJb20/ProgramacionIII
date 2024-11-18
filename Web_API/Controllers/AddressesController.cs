using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressesController : ControllerBase
    {
        private readonly IAddressesService _service;

        public AddressesController(IAddressesService service)
        {
            _service = service;
        }

        [HttpGet("Countries")]
        public async Task<ActionResult<ResponseManager<CountriesView>>> GetCountries()
        {
            var response = await _service.GetAllCountries();
            return Ok(response);
        }

        [HttpGet("States")]
        public async Task<ActionResult<ResponseManager<CountryStatesView>>> GetStates()
        {
            var response = await _service.GetAllStates();
            return Ok(response);
        }
    }
}
