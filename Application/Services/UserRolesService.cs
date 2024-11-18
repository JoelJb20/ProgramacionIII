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
    public class UserRolesService : IUserRolesService
    {
        private readonly IUserRolesRepository _repository;

        public UserRolesService(IUserRolesRepository repository)
        {
            _repository = repository;
        }

       

        public async Task<ResponseManager<UserRolesView>> GetAll()
        {
            return await _repository.GetAll();
        }


        public async Task<ResponseManager<UserRolesView>> GetById(int roleId)
        {
            return await _repository.GetById(roleId);
        }

        public async Task<ResponseManager<UserRoleDTO>> Insert(UserRoleDTO model)
        {
            return await _repository.Insert(model);
        }

        public async Task<ResponseManager<UserRoleDTO>> Update(UserRoleDTO model)
        {
            return await _repository.Update(model);
        }

        public async Task<ResponseManager> Delete(int roleId, int userId)
        {
            return await _repository.Delete(roleId, userId);
        }
    }
}
