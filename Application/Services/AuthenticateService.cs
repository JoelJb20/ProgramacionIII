using Application.DTO;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepository _repository;

        public AuthenticateService(IAuthenticateRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseManager<TokenDTO>> DoLogin(LoginDTO model)
        {
            return await _repository.DoLogin(model);
        }
        public async Task<ResponseManager> DoSignUp(SignUpDTO model)
        {
            return await _repository.DoSignUp(model);
        }
    }
}
