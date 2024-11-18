using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICinemaRepository
    {
        Task<ResponseManager<CinemasView>> GetAllCinemas();
        Task<ResponseManager<CinemasView>> GetCinemasByName(string cinemaName);
        Task<ResponseManager<CinemasView>> GetCinemasWithMoviesAssignedByMovieId(int movieId);
        Task<ResponseManager> Delete(int cinemaId, int userId);
        Task<ResponseManager<CinemaDTO>> Update(CinemaDTO cine);
        Task<ResponseManager<CinemaDTO>> Create (CinemaDTO cine);
        Task<ResponseManager<CinemasView>> GetById(int cinemaId);
    }
}
