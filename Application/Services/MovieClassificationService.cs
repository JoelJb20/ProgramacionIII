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
    public class MovieClassificationService : IMovieClassificationService
    {
        private readonly IMovieClassificationRepository _repository;

        public MovieClassificationService(IMovieClassificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseManager<MovieClassificationsView>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}
