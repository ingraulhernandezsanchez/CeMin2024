using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Application.DTOs
{
    public class RoleDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool Activo { get; set; }
        public string? CodigoAplicacion { get; set; }

        public RoleDto()
        {
        }

        public RoleDto(string nombre)
        {
            Nombre = nombre;
            Activo = true;
        }
    }
}
