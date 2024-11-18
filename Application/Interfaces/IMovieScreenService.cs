using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public interface IMovieScreenService
    {
        Task<ResponseManager<MoviesByScreensView>> GetAllMoviesInScreen();
        Task<ResponseManager<MoviesByScreensView>> GetMoviesByCinemaId(int cinemaId);
        Task<ResponseManager<MoviesByScreensView>> GetMoviesByScreenId(int screenId);
        Task<ResponseManager<MoviesByScreensView>> GetMoviesByMovieId(int movieId);
        Task<ResponseManager> DeleteMovieInScreen(int movieByScreenId, int userId);
        Task<ResponseManager<MoviesByScreenDTO>> InsertMovieByScreen(MoviesByScreenDTO model);
        Task<ResponseManager<MoviesByScreenDTO>> UpdateMovieByScreen(MoviesByScreenDTO model);
    }
}
