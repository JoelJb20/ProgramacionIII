using Application.DTO;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Infrastructure.Repositories
{
    public class CinemaScreenRepository : ICinemaScreenRepository
    {
        private readonly CinemaContext _context;

        public CinemaScreenRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }


        public async Task<ResponseManager<CinemaScreensView>> GetAllScreens()
        {
            var response = new ResponseManager<CinemaScreensView>();
            try
            {
                var list = await _context.CinemaScreensViews.Where(x => x.IsRecordActive == true)
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

        public async Task<ResponseManager<CinemaScreensView>> GetScreensByCinemaId(int cinemaId)
        {
            var response = new ResponseManager<CinemaScreensView>();
            try
            {
                var list = await _context.CinemaScreensViews.Where(x => x.IsRecordActive == true && x.CinemaId == cinemaId)
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

        public async Task<ResponseManager<CinemaScreenDTO>> InsertScreen(CinemaScreenDTO model)
        {
            var response = new ResponseManager<CinemaScreenDTO>();
            try
            {
                var cinema = await _context.CinemasViews.Where(x=> x.CinemaId == model.CinemaId).FirstOrDefaultAsync();

                if(cinema == null)
                {
                    throw new CustomException($"El cine seleccionado no ha sido encontrado", null, HttpStatusCode.BadRequest);
                }

                var newScreen = new CinemaScreen
                {
                    ScreenName = model.ScreenName,
                    CinemaId = model.CinemaId == 0 ? null : model.CinemaId,
                    GeneralPriceBySeat = model.GeneralPriceBySeat,
                    Seats = model.Seats,  
                    CreatedByUserId = model.UserId,
                    CreatedAt = DateTime.Now,
                    IsRecordActive = true
                };

                await _context.CinemaScreens.AddAsync(newScreen);
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

        public async Task<ResponseManager<CinemaScreenDTO>> UpdateScreen(CinemaScreenDTO model)
        {
            var response = new ResponseManager<CinemaScreenDTO>();
            try
            {

                await _context.CinemaScreens.Where(x => x.ScreenId == model.ScreenId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.ScreenName, model.ScreenName)
                        .SetProperty(p => p.CinemaId, model.CinemaId)
                        .SetProperty(p => p.Seats, model.Seats)
                        .SetProperty(p => p.GeneralPriceBySeat, model.GeneralPriceBySeat)
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


        public async Task<ResponseManager> DeleteScreen(int screenId, int userId)
        {
            var response = new ResponseManager();
            try
            {

                await _context.CinemaScreens.Where(x => x.ScreenId == screenId)
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
