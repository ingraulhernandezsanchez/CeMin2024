using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.Common
{
    public static class Constants
    {
        public static class Configuration
        {
            public const string NombreAplicacionKey = "NombreAplicacion";
            public const string ConnectionStringKey = "ConnectionSAU";
        }

        public static class Roles
        {
            public const string DefaultRole = "Usuario";
            public const string AdminRole = "Administrador";
        }
    }
}
