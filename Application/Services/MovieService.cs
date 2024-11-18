using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository repository)
        {
            _movieRepository = repository;
        }


        public async Task<ResponseManager<MoviesView>> GetAllMovies()
        {
            return await _movieRepository.GetAllMovies();
        }

        public async Task<ResponseManager<MoviesView>> GetAllMoviesAssigned()
        {
            return await _movieRepository.GetAllMoviesAssigned();
        }

        public async Task<ResponseManager<MoviesView>> GetMoviesByName(string name)
        {
            return await _movieRepository.GetMoviesByName(name);
        }

        public async Task<ResponseManager> Delete(int id, int userId)
        {
            return await _movieRepository.Delete(id, userId);
        }

        public async Task<ResponseManager<MovieDTO>> Update(MovieDTO cine)
        {
            return await _movieRepository.Update(cine);
        }

        public async Task<ResponseManager<MovieDTO>> Create(MovieDTO cine)
        {
            return await _movieRepository.Create(cine);
        }

        public async Task<ResponseManager> UploadImage(MovieImageDTO model)
        {

            return await _movieRepository.UploadImage(model);
        }

        public async Task<ResponseManager> DeleteImage(int movieId, int userId)
        {
            return await _movieRepository.DeleteImage(movieId, userId);

        }

        public async Task<ResponseManager<MoviesView>> GetById(int id)
        {
            return await _movieRepository.GetById(id);
        }

        public async Task<ResponseManager<ActorsInMoviesView>> GetActorsByMovie(int movieId)
        {
            return await _movieRepository.GetActorsByMovie(movieId);
        }

        public async Task<ResponseManager> DeleteActorInMovie(int acInMoId, int userId)
        {
            return await _movieRepository.DeleteActorInMovie(acInMoId, userId);
        }
    }
}
