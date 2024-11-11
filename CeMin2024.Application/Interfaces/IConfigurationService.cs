using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Interfaces
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Obtiene el nombre de la aplicación desde la configuración
        /// </summary>
        /// <returns>Nombre de la aplicación</returns>
        Task<string> GetApplicationName();
    }
}
