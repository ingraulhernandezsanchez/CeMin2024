using CeMin2024.Application.Interfaces;
using CeMin2024.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using CeMin2024.Domain.Models;

namespace CeMin2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IRoleService _roleService;

        public AuthController(
            IRoleService roleService,
            ILogger<AuthController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                // Aquí implementarías la lógica de autenticación real
                if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                {
                    return BadRequest("Usuario y contraseña son requeridos");
                }

                // Verificar que el rol existe
                var roleExists = await _roleService.ExisteRolAsync(model.SelectedRole);
                if (!roleExists)
                {
                    return BadRequest("El rol seleccionado no es válido");
                }

                // Aquí irían las validaciones contra tu sistema de autenticación

                return Ok(new { token = "token-ejemplo", username = model.Username, role = model.SelectedRole });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el login");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                // Implementa la lógica de logout si es necesaria
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error durante el logout");
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet("status")]
        public IActionResult CheckAuthStatus()
        {
            try
            {
                // Implementa la verificación del estado de autenticación
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al verificar el estado de autenticación");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}