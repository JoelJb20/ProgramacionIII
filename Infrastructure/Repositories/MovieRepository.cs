using Application.DTO;
using Application.Interfaces;
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
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaContext _context;
        public MovieRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }

        public async Task<ResponseManager<MoviesView>> GetAllMovies()
        {
            var response = new ResponseManager<MoviesView>();
            try
            {
                var movies = await _context.MoviesViews.Where(x => x.IsRecordActive == true)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
                response.DataList = movies;
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

        public async Task<ResponseManager<MoviesView>> GetAllMoviesAssigned()
        {
            var response = new ResponseManager<MoviesView>();
            try
            {
                var movies = await _context.MoviesViews.Where(m => m.IsRecordActive == true)
                    .Join(_context.MoviesByScreens.Where(ms=>ms.IsRecordActive == true),
                        m => m.MovieId, // Primary key of Movies
                        ms => ms.MovieId, // Foreign key in MoviesByScreens
                        (m, ms) => m) // Select only the movies
                    .Distinct()
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();

                response.DataList = movies;
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

        public async Task<ResponseManager<MoviesView>> GetMoviesByName(string name)
        {
            var response = new ResponseManager<MoviesView>();
            try
            {
                var movies = await _context.MoviesViews.Where(x => x.IsRecordActive == true &&
                                                              x.MovieName.Contains(name))
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
                response.DataList = movies;
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

        public async Task<ResponseManager> Delete(int id, int userId)
        {

            var response = new ResponseManager();
            try
            {

                await _context.Movies.Where(x => x.MovieId == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.IsRecordActive, false)
                        .SetProperty(p => p.LastModificationByUserId, userId)
                        .SetProperty(p => p.LastModificationAt, DateTime.Now));

                await _context.SaveChangesAsync();
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

        public async Task<ResponseManager<MovieDTO>> Update(MovieDTO model)
        {
            var response = new ResponseManager<MovieDTO>();
            try
            {
                await _context.Database.BeginTransactionAsync();


                var movie = await _context.Movies.FirstOrDefaultAsync(x => x.MovieId == model.MovieId);
                if (movie == null)
                {
                    throw new CustomException($"La pelicula seleccionada no ha podido ser encontrada.", null, HttpStatusCode.BadRequest);
                }

                movie.MovieName = model.MovieName;
                movie.ClassificationId = model.ClassificationId == 0 ? null : model.ClassificationId;
                movie.GenderId = model.GenderId == 0 ? null : model.GenderId;
                movie.DirectorName = model.DirectorName;
                movie.ReleaseDate = model.ReleaseDate;
                movie.ReleaseHour = model.ReleaseHour;
                movie.Synopsis = model.Synopsis;
               
                movie.LastModificationByUserId = model.UserId;
                movie.LastModificationAt = DateTime.Now;


                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();

                foreach (var actor in model.ActorsInMovies)
                {
                    actor.MovieId = model.MovieId;
                    var actorResponse = new ResponseManager<ActorsInMovieDTO>();

                    if (actor.AcInMoId == 0)
                    {
                        actorResponse = await CreateActorInMovie(actor);
                    }
                    else
                    {
                        actorResponse = await UpdateActorInMovie(actor);
                    }

                    if (!actorResponse.Succeded)
                    {
                        await _context.Database.RollbackTransactionAsync();
                        response.Warnings.AddRange(actorResponse.Warnings);
                        return response;
                    }
                }

                response.SingleData = model;

                await _context.Database.CommitTransactionAsync();
            }
            catch (CustomException e)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<ResponseManager<MovieDTO>> Create(MovieDTO model)
        {
            var response = new ResponseManager<MovieDTO>();
            try
            {
                await _context.Database.BeginTransactionAsync();

                var newMovie = new Movie
                {
                    MovieName = model.MovieName,
                    GenderId = model.GenderId == 0 ? null : model.GenderId,
                    ClassificationId = model.ClassificationId == 0 ? null : model.ClassificationId,
                    Synopsis = model.Synopsis,
                    DirectorName = model.DirectorName,
                    ReleaseDate = model.ReleaseDate,
                    ReleaseHour = model.ReleaseHour,
                    CreatedByUserId = model.UserId,
                    CreatedAt = DateTime.Now,
                    IsRecordActive = true
                };

                await _context.Movies.AddAsync(newMovie);
                await _context.SaveChangesAsync();

                model.MovieId = newMovie.MovieId;

                foreach (var actor in model.ActorsInMovies)
                {
                    actor.MovieId = newMovie.MovieId;

                    var actorResponse = await CreateActorInMovie(actor);
                    if (!actorResponse.Succeded)
                    {
                        await _context.Database.RollbackTransactionAsync();
                        response.Warnings.AddRange(actorResponse.Warnings);
                        return response;
                    }
                }

                response.SingleData = model;

                await _context.Database.CommitTransactionAsync();
            }
            catch (CustomException e)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<ResponseManager> UploadImage(MovieImageDTO model)
        {

            var response = new ResponseManager();
            try
            {
                await _context.Database.BeginTransactionAsync();


                var movie = await _context.Movies.FirstOrDefaultAsync(x => x.MovieId == model.MovieId);
                if (movie == null)
                {
                    throw new CustomException($"La pelicula seleccionada no ha podido ser encontrada.", null, HttpStatusCode.BadRequest);
                }

                movie.LastModificationByUserId = model.UserId;
                movie.LastModificationAt = DateTime.Now;


                string? fileName = null;
                string? fileExtension = null;
                string? fileContentBase64 = null;

                if (model.ImageUploaded != null || model.ImageUploaded?.Length > 0)
                {
                    if (model.ImageUploaded.Length > (5 * 1024 * 1024))
                    {
                        throw new CustomException($"El archivo es demasiado grande. El tamaño máximo permitido es de 5MB.", null, HttpStatusCode.BadRequest);
                    }

                    // Convertir el archivo en un array de bytes
                    byte[] fileBytes;
                    await using (var ms = new MemoryStream())
                    {
                        await model.ImageUploaded.CopyToAsync(ms);
                        fileBytes = ms.ToArray();
                    }

                    // Separar el nombre del archivo y la extensión
                    fileName = Path.GetFileNameWithoutExtension(model.ImageUploaded.FileName);
                    fileExtension = Path.GetExtension(model.ImageUploaded.FileName);
                    fileContentBase64 = Convert.ToBase64String(fileBytes);

                    movie.ImageBytes = fileContentBase64;
                    movie.ImageName = fileName;
                    movie.ImageExtension = fileExtension;

                }

                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();


                await _context.Database.CommitTransactionAsync();
            }
            catch (CustomException e)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<ResponseManager> DeleteImage(int movieId, int userId)
        {

            var response = new ResponseManager();
            try
            {
                await _context.Database.BeginTransactionAsync();


                var movie = await _context.Movies.FirstOrDefaultAsync(x => x.MovieId == movieId);
                if (movie == null)
                {
                    throw new CustomException($"La pelicula seleccionada no ha podido ser encontrada.", null, HttpStatusCode.BadRequest);
                }

                movie.ImageBytes = null;
                movie.ImageName = null;
                movie.ImageExtension = null;
                movie.LastModificationByUserId = userId;
                movie.LastModificationAt = DateTime.Now;


                _context.Movies.Update(movie);
                await _context.SaveChangesAsync();


                await _context.Database.CommitTransactionAsync();
            }
            catch (CustomException e)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new CustomException(e.Message, e.InnerException, e.StatusCode, e.ClassName, e.MethodName, e.CreationUserId);
            }
            catch (Exception ex)
            {
                if (_context.Database.CurrentTransaction != null)
                {
                    await _context.Database.RollbackTransactionAsync();
                }

                throw new Exception(ex.Message, ex.InnerException);
            }

            return response;
        }

        public async Task<ResponseManager<MoviesView>> GetById(int id)
        {
            var response = new ResponseManager<MoviesView>();
            try
            {
                var movieUpd = await _context.MoviesViews.FirstOrDefaultAsync(a => a.MovieId == id);

                response.SingleData = movieUpd;
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

        public async Task<ResponseManager<ActorsInMoviesView>> GetActorsByMovie(int movieId)
        {
            var response = new ResponseManager<ActorsInMoviesView>();
            try
            {
                var actors = await _context.ActorsInMoviesViews.Where(x => x.IsRecordActive == true &&
                                                              x.MovieId == movieId)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
                response.DataList = actors;
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
        public async Task<ResponseManager<ActorsInMovieDTO>> CreateActorInMovie(ActorsInMovieDTO model)
        {
            var response = new ResponseManager<ActorsInMovieDTO>();
            try
            {
                var newActor = new ActorsInMovie
                {
                    ActorFirstname = model.ActorFirstname,
                    ActorLastname = model.ActorLastname,
                    MovieId = model.MovieId,
                    CreatedByUserId = model.UserId,
                    CreatedAt = DateTime.Now,
                    IsRecordActive = true
                };

                await _context.ActorsInMovies.AddAsync(newActor);
                await _context.SaveChangesAsync();

                model.AcInMoId = newActor.AcInMoId;

                response.SingleData = model;

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
        public async Task<ResponseManager<ActorsInMovieDTO>> UpdateActorInMovie(ActorsInMovieDTO model)
        {
            var response = new ResponseManager<ActorsInMovieDTO>();
            try
            {
                await _context.ActorsInMovies.Where(x => x.AcInMoId == model.AcInMoId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.ActorFirstname, model.ActorFirstname)
                        .SetProperty(p => p.ActorLastname, model.ActorLastname)
                        .SetProperty(p => p.LastModificationByUserId, model.UserId)
                        .SetProperty(p => p.LastModificationAt, DateTime.Now));

                await _context.SaveChangesAsync();

                response.SingleData = model;
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
        public async Task<ResponseManager> DeleteActorInMovie(int acInMoId, int userId)
        {
            var response = new ResponseManager();
            try
            {

                await _context.ActorsInMovies.Where(x => x.AcInMoId == acInMoId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.IsRecordActive, false)
                        .SetProperty(p => p.LastModificationByUserId, userId)
                        .SetProperty(p => p.LastModificationAt, DateTime.Now));

                await _context.SaveChangesAsync();
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
