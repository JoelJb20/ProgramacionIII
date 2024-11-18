using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public interface ICinemaScreenRepository
    {
        Task<ResponseManager<CinemaScreensView>> GetAllScreens();
        Task<ResponseManager<CinemaScreensView>> GetScreensByCinemaId(int cinemaId);
        Task<ResponseManager> DeleteScreen(int screenId, int userId);
        Task<ResponseManager<CinemaScreenDTO>> InsertScreen(CinemaScreenDTO model);
        Task<ResponseManager<CinemaScreenDTO>> UpdateScreen(CinemaScreenDTO model);
    }
}
