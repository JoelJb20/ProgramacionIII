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
    public class CinemaScreenService : ICinemaScreenService
    {
        private readonly ICinemaScreenRepository _screenRepository;

        public CinemaScreenService(ICinemaScreenRepository repository)
        {
            _screenRepository = repository;
        }

       

        public async Task<ResponseManager<CinemaScreensView>> GetAllScreens()
        {
            return await _screenRepository.GetAllScreens();
        }

        public async Task<ResponseManager<CinemaScreensView>> GetScreensByCinemaId(int cinemaId)
        {
            return await _screenRepository.GetScreensByCinemaId( cinemaId);
        }

        public async Task<ResponseManager<CinemaScreenDTO>> InsertScreen(CinemaScreenDTO model)
        {
            return await _screenRepository.InsertScreen( model);
        }

        public async Task<ResponseManager<CinemaScreenDTO>> UpdateScreen(CinemaScreenDTO model)
        {
            return await _screenRepository.UpdateScreen( model);
        }

        public async Task<ResponseManager> DeleteScreen(int screenId, int userId)
        {
            return await _screenRepository.DeleteScreen( screenId,  userId);
        }
    }
}
