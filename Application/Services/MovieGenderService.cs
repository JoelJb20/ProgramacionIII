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
    public class MovieGenderService : IMovieGenderService
    {
        private readonly IMovieGenderRepository _repository;

        public MovieGenderService(IMovieGenderRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseManager<MovieGendersView>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
