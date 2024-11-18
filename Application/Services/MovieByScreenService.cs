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
    public class MovieByScreenService : IMovieScreenService
    {
        private readonly IMovieScreenRepository _screenRepository;

        public MovieByScreenService(IMovieScreenRepository repository)
        {
            _screenRepository = repository;
        }

        public async Task<ResponseManager<MoviesByScreensView>> GetAllMoviesInScreen()
        {
            return await _screenRepository.GetAllMoviesInScreen();
        }

        public async Task<ResponseManager<MoviesByScreensView>> GetMoviesByCinemaId(int cinemaId)
        {
            return await _screenRepository.GetMoviesByCinemaId(cinemaId);
        }

        public async Task<ResponseManager<MoviesByScreensView>> GetMoviesByScreenId(int screenId)
        {
            return await _screenRepository.GetMoviesByScreenId(screenId);
        }

        public async Task<ResponseManager<MoviesByScreensView>> GetMoviesByMovieId(int movieId)
        {
            return await _screenRepository.GetMoviesByMovieId(movieId);
        }

        public async Task<ResponseManager<MoviesByScreenDTO>> InsertMovieByScreen(MoviesByScreenDTO model)
        {
            return await _screenRepository.InsertMovieByScreen(model);
        }

        public async Task<ResponseManager<MoviesByScreenDTO>> UpdateMovieByScreen(MoviesByScreenDTO model)
        {
            return await _screenRepository.UpdateMovieByScreen(model);
        }

        public async Task<ResponseManager> DeleteMovieInScreen(int movieByScreenId, int userId)
        {
            return await _screenRepository.DeleteMovieInScreen(movieByScreenId, userId);
        }
    }
}
