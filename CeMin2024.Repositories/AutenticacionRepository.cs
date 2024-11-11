using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeMin2024.Repositories
{
    public class AutenticacionRepository : IAutenticacionRepository
    {

        private readonly IDbConnection _dbConnection;
        public AutenticacionRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<SesionDTO> Login(UserInfo user)
        {

            var sesion = new SesionDTO();

            try
            {
                //using (PrincipalContext context = new PrincipalContext(ContextType.Domain,"LDAP://ins.gov.co"))
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, "ins.local", user.UserName, user.Password))
                {
                    sesion.Autenticado = context.ValidateCredentials(user.UserName, user.Password);
                    var usuarioINS = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, user.UserName);

                    if (sesion.Autenticado)
                    {
                        var sql = @"SELECT GTH.ID,GTH.ID_Perfil,GTH.usuario,GTH.TipoID,GTH.NumID,GTH.Nombres,GTH.Apellidos,GTH.FechaInicioContrato,GTH.FechaFinContrato,P.Perfil
                        FROM GrupoTalentoHumano GTH INNER JOIN Perfil P on GTH.ID_Perfil= P.ID 
                        WHERE GTH.usuario = @usuario AND  GTH.Activo=1";

                        var usuario = await _dbConnection.QueryFirstOrDefaultAsync<UsuarioGTH>(sql, new { usuario = user.UserName });

                        if (usuario != null)
                        {
                            sesion.Autenticado = true;
                            sesion.ID = usuario.ID;
                            sesion.Nombre = usuario.Nombres + " " + usuario.Apellidos;
                            sesion.Email = usuario.Usuario;
                            sesion.Perfil = usuario.Perfil;
                            sesion.NombrePerfil = usuario.Perfil;
                            sesion.Email = usuarioINS.EmailAddress;
                        }
                        else
                        {
                            sesion.ID = 0;
                            sesion.Autenticado = true;
                            if (usuarioINS != null)
                            {
                                try
                                {
                                    sesion.Email = usuarioINS.EmailAddress; ;
                                    sesion.Nombre = usuarioINS.DisplayName;
                                    sesion.Perfil = "anonimus";
                                    sesion.NombrePerfil = "anonimus";

                                }
                                catch (Exception e)
                                {
                                    sesion.Error = true;
                                    sesion.Autenticado = false;
                                    sesion.Email = "Error";
                                    sesion.Nombre = "Error";
                                    sesion.Perfil = "Error";
                                    sesion.NombrePerfil = "Error";
                                    sesion.DesError = e.Message;
                                }
                            }

                        }
                    }
                    else
                    {
                        sesion.Autenticado = false;
                    }
                    usuarioINS.Dispose();
                }
            }
            catch (Exception e)
            {
                sesion.Autenticado = false;
                sesion.Error = true;
                sesion.DesError = e.Message;
                return sesion;
            }

            return sesion;
        }
    }
}
