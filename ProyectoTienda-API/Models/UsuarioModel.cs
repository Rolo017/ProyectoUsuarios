using Dapper;
using Microsoft.IdentityModel.Tokens;
using ProyectoTienda_API.Entities;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectoTienda_API.Models
{
    public class UsuarioModel
    {

        public UsuarioObj? ValidarUsuario(UsuarioObj usuario, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var datos= connection.Query<UsuarioObj>("ValidarCredenciales", 
                    new {usuario.Usuario,usuario.Contrasenna }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                
                if(datos != null)
                {
                    datos.Token = CrearToken(usuario.Usuario);
                    return datos;
                }
                return null;
               
            }
        }
        public int RegistrarUsuario(UsuarioObj usuario, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("CrearUsuario",
                    new { usuario.Nombre,usuario.Cedula,usuario.Telefono,usuario.Direccion,usuario.Correo, usuario.TipoUsuario,usuario.IdRol }, commandType: CommandType.StoredProcedure);

            }
        }
        private string CrearToken(string Usuario)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Usuario)
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916"));
            var cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
