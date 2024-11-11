using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Repositories
{
    public interface IAutenticacionRepository
    {
        Task<SesionDTO> Login(UserInfo user);
    }
}
