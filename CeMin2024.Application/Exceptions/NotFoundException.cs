using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string name, object key)
            : base($"Entidad \"{name}\" ({key}) no fue encontrada.")
        {
        }
    }
}
