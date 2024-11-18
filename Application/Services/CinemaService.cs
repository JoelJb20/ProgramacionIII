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
    public class CinemaService : ICinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository repository)
        {
            _cinemaRepository = repository;
        }

        public async Task<ResponseManager<CinemasView>> GetCinemasByName(string cinemaName)
        {
            return await _cinemaRepository.GetCinemasByName(cinemaName);
        }

        public async Task<ResponseManager<CinemasView>> GetCinemasWithMoviesAssignedByMovieId(int movieId)
        {
            return await _cinemaRepository.GetCinemasWithMoviesAssignedByMovieId(movieId);
        }
        public async Task<ResponseManager<CinemasView>> GetAllCinemas()
        {
            return await _cinemaRepository.GetAllCinemas();
           
        }
        public async Task<ResponseManager> Delete(int cinemaId, int userId)
        {
            return await _cinemaRepository.Delete(cinemaId, userId);
        }

        public async Task<ResponseManager<CinemaDTO>> Update(CinemaDTO cine)
        {
            return await _cinemaRepository.Update(cine);
        }

        public async Task<ResponseManager<CinemaDTO>> Create(CinemaDTO cine)
        {
            return await _cinemaRepository.Create(cine);
        }

        public async Task<ResponseManager<CinemasView>> GetById(int cinemaId)
        {
            return await _cinemaRepository.GetById(cinemaId);
        }
    }
}
