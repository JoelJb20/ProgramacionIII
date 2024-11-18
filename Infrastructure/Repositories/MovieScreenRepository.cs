using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Repositories
{
    public class MovieScreenRepository : IMovieScreenRepository
    {
        private readonly CinemaContext _context;

        public MovieScreenRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }

       

        public async Task<ResponseManager<MoviesByScreensView>> GetAllMoviesInScreen()
        {
            var response = new ResponseManager<MoviesByScreensView>();
            try
            {
                var list = await _context.MoviesByScreensViews.Where(x => x.IsRecordActive == true)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
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

        public async Task<ResponseManager<MoviesByScreensView>> GetMoviesByCinemaId(int cinemaId)
        {
            var response = new ResponseManager<MoviesByScreensView>();
            try
            {
                var list = await _context.MoviesByScreensViews.Where(x => x.IsRecordActive == true && x.CinemaId == cinemaId)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
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

        public async Task<ResponseManager<MoviesByScreensView>> GetMoviesByScreenId(int screenId)
        {
            var response = new ResponseManager<MoviesByScreensView>();
            try
            {
                var list = await _context.MoviesByScreensViews.Where(x => x.IsRecordActive == true && x.ScreenId == screenId)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
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

        public async Task<ResponseManager<MoviesByScreensView>> GetMoviesByMovieId(int movieId)
        {
            var response = new ResponseManager<MoviesByScreensView>();
            try
            {
                var list = await _context.MoviesByScreensViews.Where(x => x.IsRecordActive == true && x.MovieId == movieId)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
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

        public async Task<ResponseManager<MoviesByScreenDTO>> InsertMovieByScreen(MoviesByScreenDTO model)
        {
            var response = new ResponseManager<MoviesByScreenDTO>();
            try
            {
                var screen  = await _context.CinemaScreens.Where(x => x.ScreenId == model.ScreenId).FirstOrDefaultAsync();

                if (screen == null)
                {
                    throw new CustomException($"La sala seleccionada no ha sido encontrada", null, HttpStatusCode.BadRequest);
                }

                var movie = await _context.Movies.Where(x => x.MovieId == model.MovieId).FirstOrDefaultAsync();

                if (movie == null)
                {
                    throw new CustomException($"La pelicula seleccionada no ha sido encontrada", null, HttpStatusCode.BadRequest);
                }

                var exisitingMovie = await _context.MoviesByScreensViews.Where(x => 
                x.MovieId == model.MovieId && 
                x.ScreenId == model.ScreenId && 
                x.ShowingDate == model.ShowingDate && 
                x.ShowingHour == model.ShowingHour && x.IsRecordActive == true).FirstOrDefaultAsync();

                if (exisitingMovie != null)
                {
                    throw new CustomException($"La pelicula seleccionada actualmente se encuentra asignada a la sala seleccionada en este mismo horario, favor de realizar otra combinación", null, HttpStatusCode.BadRequest);
                }

                var newScreen = new MoviesByScreen
                {
                    ScreenId = model.ScreenId,
                    MovieId = model.MovieId,
                    ShowingDate = model.ShowingDate,
                    ShowingHour = model.ShowingHour,
                    PriceBySeat = model.PriceBySeat,
                    IsHoliday   = model.IsHoliday  ,
                    HolidayName = model.HolidayName,
                    CreatedByUserId = model.UserId,
                    CreatedAt = DateTime.Now,
                    IsRecordActive = true
                };

                await _context.MoviesByScreens.AddAsync(newScreen);
                await _context.SaveChangesAsync();

                model.ScreenId = newScreen.ScreenId;
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

        public async Task<ResponseManager<MoviesByScreenDTO>> UpdateMovieByScreen(MoviesByScreenDTO model)
        {
            var response = new ResponseManager<MoviesByScreenDTO>();
            try
            {

                var exisitingMovie = await _context.MoviesByScreensViews.Where(x =>
                x.MovieId == model.MovieId &&
                x.ScreenId == model.ScreenId &&
                x.ShowingDate == model.ShowingDate &&
                x.ShowingHour == model.ShowingHour && 
                x.IsRecordActive == true && 
                x.MovieByScreenId != model.MovieByScreenId).FirstOrDefaultAsync();

                if (exisitingMovie != null)
                {
                    throw new CustomException($"La pelicula seleccionada actualmente se encuentra asignada a esta misma sala en este mismo horario, favor de realizar otra combinación", null, HttpStatusCode.BadRequest);
                }

                await _context.MoviesByScreens.Where(x => x.MovieByScreenId == model.MovieByScreenId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.ScreenId, model.ScreenId)
                        .SetProperty(p => p.ShowingDate, model.ShowingDate)
                        .SetProperty(p => p.ShowingHour, model.ShowingHour)
                        .SetProperty(p => p.PriceBySeat, model.PriceBySeat)
                        .SetProperty(p => p.IsHoliday, model.IsHoliday)
                        .SetProperty(p => p.HolidayName, model.HolidayName)
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

        public async Task<ResponseManager> DeleteMovieInScreen(int movieByScreenId, int userId)
        {
            var response = new ResponseManager();
            try
            {

                await _context.MoviesByScreens.Where(x => x.MovieByScreenId == movieByScreenId)
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
