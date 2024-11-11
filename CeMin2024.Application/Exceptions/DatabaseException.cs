using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Exceptions
{
    public class DatabaseException : BaseException
    {
        public DatabaseException(string message)
            : base($"Error en la base de datos: {message}")
        {
        }

        public DatabaseException(string message, Exception innerException)
            : base($"Error en la base de datos: {message}", innerException)
        {
        }
    }
}
