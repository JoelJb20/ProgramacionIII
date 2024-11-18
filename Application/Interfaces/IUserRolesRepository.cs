using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<ResponseManager<UserRolesView>> GetAll();
        Task<ResponseManager<UserRolesView>> GetById(int roleId);
        Task<ResponseManager<UserRoleDTO>> Insert(UserRoleDTO model);
        Task<ResponseManager<UserRoleDTO>> Update(UserRoleDTO model);
        Task<ResponseManager> Delete(int roleId, int userId);
    }
}
