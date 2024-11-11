using System.Data;
using CeMin2024.Application.Interfaces;
using CeMin2024.Domain.Models;
using CeMin2024.Application.Exceptions;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace CeMin2024.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IDbConnection _dbConnection;
        private readonly IConfiguration _configuration;
        private readonly string _nombreAplicacion;
        private const string BaseQuery = @"
            SELECT [NOMBRE]
            FROM [Core.Admin].[dbo].[PERFIL] PE 
            WHERE PE.CODIGO_APLICACION = (
                SELECT TOP 1 [CODIGO_APLICACION] 
                FROM [Core.Admin].[dbo].[APLICACION] ap 
                WHERE ap.NOMBRE = @NombreApp 
                AND AP.ESTADO = 1
            )";

        public RoleService(IDbConnection dbConnection, IConfiguration configuration)
        {
            _dbConnection = dbConnection ?? throw new ArgumentNullException(nameof(dbConnection));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _nombreAplicacion = _configuration["NombreAplicacion"]
                ?? throw new ConfigurationException("La configuración 'NombreAplicacion' no está definida");
        }

        public async Task<List<RoleModel>> GetRolesAsync()
        {
            try
            {
                var roles = await _dbConnection.QueryAsync<RoleModel>(
                    BaseQuery,
                    new { NombreApp = _nombreAplicacion }
                );

                return roles?.ToList() ?? new List<RoleModel>();
            }
            catch (Exception ex)
            {
                throw new DatabaseException(
                    $"Error al obtener los roles para la aplicación {_nombreAplicacion}",
                    ex);
            }
        }

        public async Task<RoleModel?> GetRoleByNombreAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del rol no puede estar vacío", nameof(nombre));

            try
            {
                var query = $"{BaseQuery} AND PE.NOMBRE = @Nombre";

                var role = await _dbConnection.QueryFirstOrDefaultAsync<RoleModel>(
                    query,
                    new
                    {
                        Nombre = nombre,
                        NombreApp = _nombreAplicacion
                    }
                );

                return role;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(
                    $"Error al obtener el rol '{nombre}' para la aplicación {_nombreAplicacion}",
                    ex);
            }
        }

        public async Task<bool> ExisteRolAsync(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del rol no puede estar vacío", nameof(nombre));

            try
            {
                var role = await GetRoleByNombreAsync(nombre);
                return role != null;
            }
            catch (DatabaseException)
            {
                // Relanzo excepciones de base de datos
                throw;
            }
            catch (Exception ex)
            {
                throw new DatabaseException(
                    $"Error al verificar la existencia del rol '{nombre}'",
                    ex);
            }
        }

        // Método helper para validar la conexión
        private void ValidateConnection()
        {
            if (_dbConnection.State != ConnectionState.Open)
            {
                try
                {
                    _dbConnection.Open();
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("No se pudo establecer la conexión con la base de datos", ex);
                }
            }
        }
    }
}