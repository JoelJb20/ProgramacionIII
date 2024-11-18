using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMovieRepository
    {
        Task<ResponseManager<MoviesView>> GetAllMovies();

        Task<ResponseManager<MoviesView>> GetAllMoviesAssigned();

        Task<ResponseManager<MoviesView>> GetMoviesByName(string movieName);
        Task<ResponseManager> Delete(int movieId, int userId);
        Task<ResponseManager<MovieDTO>> Update(MovieDTO model);
        Task<ResponseManager<MovieDTO>> Create (MovieDTO model);
        Task<ResponseManager<MoviesView>> GetById(int movieId);
        Task<ResponseManager> UploadImage(MovieImageDTO model);
        Task<ResponseManager> DeleteImage(int movieId, int userId);
        Task<ResponseManager<ActorsInMoviesView>> GetActorsByMovie(int movieId);
        Task<ResponseManager<ActorsInMovieDTO>> CreateActorInMovie(ActorsInMovieDTO model);
        Task<ResponseManager<ActorsInMovieDTO>> UpdateActorInMovie(ActorsInMovieDTO model);
        Task<ResponseManager> DeleteActorInMovie(int acInMoId, int userId);
    }
}
