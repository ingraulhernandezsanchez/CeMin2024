using Microsoft.AspNetCore.Mvc;
using CeMin2024.Application.Interfaces;

namespace CeMin2024.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService _configService;
        private readonly ILogger<ConfigurationController> _logger;

        public ConfigurationController(
            IConfigurationService configService,
            ILogger<ConfigurationController> logger)
        {
            _configService = configService;
            _logger = logger;
        }

        [HttpGet("appname")]
        public async Task<ActionResult<string>> GetApplicationName()
        {
            try
            {
                var appName = await _configService.GetApplicationName();
                return Ok(appName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el nombre de la aplicación");
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}