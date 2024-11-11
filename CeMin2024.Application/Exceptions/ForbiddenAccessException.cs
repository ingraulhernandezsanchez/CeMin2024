using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Exceptions
{
    public class ForbiddenAccessException : BaseException
    {
        public ForbiddenAccessException(string message = "No tiene permisos para acceder a este recurso.")
            : base(message)
        {
        }
    }
}
