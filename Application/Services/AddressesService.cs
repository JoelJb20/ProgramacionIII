using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AddressesService : IAddressesService
    {
        private readonly IAddressesRepository _repository;

        public AddressesService(IAddressesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseManager<CountriesView>> GetAllCountries()
        {
            return await _repository.GetAllCountries();
        }

        public async Task<ResponseManager<CountryStatesView>> GetAllStates()
        {
            return await _repository.GetAllStates();
        }
    }
}
