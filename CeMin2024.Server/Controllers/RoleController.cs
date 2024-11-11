using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using CeMin2024.Application.Interfaces;
using CeMin2024.Domain.Models;

namespace CeMin2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(
            IRoleService roleService,
            ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetRolesAsync();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los roles");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("{nombre}")]
        public async Task<ActionResult<RoleModel>> GetRoleByNombre(string nombre)
        {
            try
            {
                var role = await _roleService.GetRoleByNombreAsync(nombre);
                if (role == null)
                    return NotFound($"No se encontró el rol: {nombre}");

                return Ok(role);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el rol {Nombre}", nombre);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("exists/{nombre}")]
        public async Task<ActionResult<bool>> ExisteRol(string nombre)
        {
            try
            {
                var exists = await _roleService.ExisteRolAsync(nombre);
                return Ok(exists);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar existencia del rol {Nombre}", nombre);
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}

