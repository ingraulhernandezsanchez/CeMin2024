using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Exceptions
{
    public class ConfigurationException : BaseException
    {
        public ConfigurationException(string configKey)
            : base($"La configuración '{configKey}' no fue encontrada o es inválida.")
        {
        }

        public ConfigurationException(string configKey, string message)
            : base($"Error en la configuración '{configKey}': {message}")
        {
        }
    }
}
