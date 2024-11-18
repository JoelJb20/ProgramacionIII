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
    public class UserRolesRepository : IUserRolesRepository
    {
        private readonly CinemaContext _context;
        private readonly IMapper _mapper;
        public UserRolesRepository(CinemaContext context, IMapper m) {
            _context = context;
            _mapper = m;
        }

        

        public async Task<ResponseManager<UserRolesView>> GetAll()
        {
            var response = new ResponseManager<UserRolesView>();
            try
            {

                var list = await _context.UserRolesViews.OrderByDescending(o => o.CreatedAt).ToListAsync();

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


        public async Task<ResponseManager<UserRolesView>> GetById(int roleId)
        {
            var response = new ResponseManager<UserRolesView>();
            try
            {
                var list = await _context.UserRolesViews.Where(x=>x.RoleId == roleId).OrderByDescending(o => o.CreatedAt).ToListAsync();

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

        public async Task<ResponseManager<UserRoleDTO>> Insert(UserRoleDTO model)
        {
            var response = new ResponseManager<UserRoleDTO>();
            try
            {
                var newRole = new UserRole
                {
                   RoleName = model.RoleName,
                   CreatedByUserId = model.UserId,
                   IsRecordActive = true
                };

                await _context.UserRoles.AddAsync(newRole);
                await _context.SaveChangesAsync();

                model.RoleId = newRole.RoleId;
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

        public async Task<ResponseManager<UserRoleDTO>> Update(UserRoleDTO model)
        {
            var response = new ResponseManager<UserRoleDTO>();
            try
            {
                await _context.UserRoles.Where(x => x.RoleId == model.RoleId)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.RoleName, model.RoleName)
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

        public async Task<ResponseManager> Delete(int roleId, int userId)
        {

            var response = new ResponseManager();
            try
            {

                await _context.UserRoles.Where(x=>x.RoleId == roleId)
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
