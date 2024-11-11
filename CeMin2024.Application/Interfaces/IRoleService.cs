using CeMin2024.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleModel>> GetRolesAsync();
        Task<bool> ExisteRolAsync(string nombre);
        Task<RoleModel> GetRoleByNombreAsync(string nombre);
    }
}
