using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly CinemaContext _context;
        private readonly IMapper _mapper;
        public AddressesRepository(CinemaContext context, IMapper m) {
            _context = context;
            _mapper = m;
        }


        public async Task<ResponseManager<CountriesView>> GetAllCountries()
        {
            var response = new ResponseManager<CountriesView>();
            try
            {
                var list = await _context.CountriesViews.Where(x => x.IsRecordActive == true).OrderByDescending(o => o.CreatedAt).ToListAsync();

                response.DataList = list;
            }
            catch (CustomException e)
            {
                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<ResponseManager<CountryStatesView>> GetAllStates()
        {
            var response = new ResponseManager<CountryStatesView>();
            try
            {
                var list = await _context.CountryStatesViews.Where(x => x.IsRecordActive == true).OrderByDescending(o => o.CreatedAt).ToListAsync();

                response.DataList = list;
            }
            catch (CustomException e)
            {
                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }
    }
}
