using Application.DTO;
using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticateService
    {
        Task<ResponseManager<TokenDTO>> DoLogin(LoginDTO model);
        Task<ResponseManager> DoSignUp(SignUpDTO model);
    }
}
