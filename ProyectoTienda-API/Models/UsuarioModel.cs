using Dapper;
using Microsoft.IdentityModel.Tokens;
using ProyectoTienda_API.Entities;
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
                var datos = connection.Query<UsuarioObj>("ValidarCredenciales",
                    new { usuario.Correo, usuario.Contraseña }, commandType: CommandType.StoredProcedure).FirstOrDefault();

                if (datos != null)
                {
                    datos.Token = CrearToken(usuario.Correo);
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
                    new
                    {
                        usuario.Cedula,
                        usuario.Nombre,
                        usuario.Apellidos,
                        usuario.Correo,
                        usuario.Contraseña,
                        usuario.Activo,
                        usuario.IdRol,
                        usuario.Token
                    }, commandType: CommandType.StoredProcedure);
            }
        }
        public int ActualizarUsuario(UsuarioObj usuario, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditarUsuario",
                    new
                    {
                        usuario.Cedula,
                        usuario.Nombre,
                        usuario.Apellidos,
                        usuario.Correo,
                        usuario.Contraseña,
                        usuario.Activo,
                        usuario.IdRol,
                        usuario.Token
                    }, commandType: CommandType.StoredProcedure);
            }
        }
        public int EliminarUsuario(int? _UserId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {

                return connection.Execute("EliminarUsuario",
                    new
                    {
                        _UserId
                    }, commandType: CommandType.StoredProcedure);
            }
        }
        public List<UsuarioObj> Get_Usuario(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<UsuarioObj>("SELECT * from Usuario");
                return SqlQuery.ToList();
            }
        }

        private string CrearToken(string Usuario)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, Usuario)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916"));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: cred);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
