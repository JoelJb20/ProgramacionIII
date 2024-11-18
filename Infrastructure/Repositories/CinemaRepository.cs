using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CinemaRepository : ICinemaRepository
    {
        private readonly CinemaContext _context;

        public CinemaRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }

        public async Task<ResponseManager<CinemasView>> GetAllCinemas()
        {
            var response = new ResponseManager<CinemasView>();
            try
            {
                var cines = await _context.CinemasViews.Where(x => x.IsRecordActive == true)
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
                response.DataList = cines;
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

        public async Task<ResponseManager<CinemasView>> GetCinemasByName(string cinemaName)
        {
            var response = new ResponseManager<CinemasView>();
            try
            {
                var cines = await _context.CinemasViews.Where(x => x.IsRecordActive == true &&
                                                              x.CinemaName.Contains(cinemaName))
                    .OrderByDescending(o => o.CreatedAt).ToListAsync();
                response.DataList = cines;
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
        public async Task<ResponseManager<CinemasView>> GetCinemasWithMoviesAssignedByMovieId(int movieId)
        {
            var response = new ResponseManager<CinemasView>();
            try
            {

                var cines = await _context.CinemasViews.Where(c => c.IsRecordActive == true)
                   .Join(_context.MoviesByScreensViews.Where(ms => ms.IsRecordActive == true && ms.MovieId == movieId),
                       c => c.CinemaId, // Primary key of Movies
                       ms => ms.CinemaId, // Foreign key in MoviesByScreens
                       (c, ms) => c) // Select only the movies
                   .Distinct()
                   .OrderByDescending(o => o.CreatedAt).ToListAsync();

                response.DataList = cines;
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

                await _context.Cinemas.Where(x => x.CinemaId == id)
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


        public async Task<ResponseManager<CinemaDTO>> Update(CinemaDTO model)
        {
           var response = new ResponseManager<CinemaDTO>();
           try
           {

                await _context.Cinemas.Where(x => x.CinemaId == model.CinemaId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.CinemaName, model.CinemaName)
                        .SetProperty(p => p.CountryStateId, model.CountryStateId)
                        .SetProperty(p => p.PrimaryAddress, model.PrimaryAddress)
                        .SetProperty(p => p.PhoneNumber, model.PhoneNumber)
                        .SetProperty(p => p.Email, model.Email)
                        .SetProperty(p => p.LocationLatitude, model.LocationLatitude)
                        .SetProperty(p => p.LocationLongitude, model.LocationLongitude)
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
      
        public async Task<ResponseManager<CinemaDTO>> Create(CinemaDTO model)
        {
            var response = new ResponseManager<CinemaDTO>();
            try
            {
                var newCinema = new Cinema
                {
                    CinemaName          = model.CinemaName,
                    CountryStateId      = model.CountryStateId,
                    PrimaryAddress      = model.PrimaryAddress,
                    PhoneNumber         = model.PhoneNumber,
                    Email               = model.Email,
                    LocationLatitude    = model.LocationLatitude,
                    LocationLongitude   = model.LocationLongitude,
                    CreatedByUserId     = model.UserId,
                    CreatedAt           = DateTime.Now,
                    IsRecordActive      = true
                };

                await _context.Cinemas.AddAsync(newCinema);
                await _context.SaveChangesAsync();

                model.CinemaId = newCinema.CinemaId;
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

       public async Task<ResponseManager<CinemasView>> GetById(int id)
        {
            var response = new ResponseManager<CinemasView>();
            try
            {
                var cineUpd = await _context.CinemasViews.FirstOrDefaultAsync(a => a.CinemaId == id);
                response.SingleData = cineUpd;
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
