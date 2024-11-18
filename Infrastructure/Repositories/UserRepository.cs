using Application.DTO;
using Application.Interfaces;
using AutoMapper;
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
    public class UserRepository : IUserRepository
    {
        private readonly CinemaContext _context;
        private readonly IMapper _mapper;
        public UserRepository(CinemaContext context, IMapper m) {
            _context = context;
            _mapper = m;
        }

        public async Task<ResponseManager<UsersViewDTO>> GetAllUsers()
        {
            var response = new ResponseManager<UsersViewDTO>();
            try
            {

                var list = await _context.UsersViews.Where(x=>x.IsRecordActive == true).OrderByDescending(o=>o.CreatedAt).ToListAsync();
                var users = list.Select(_mapper.Map<UsersViewDTO>).ToList();

                response.DataList = users;
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
