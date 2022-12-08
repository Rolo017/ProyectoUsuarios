using Dapper;
using ProyectoTienda_API.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTienda_API.Models
{
    public class RolesModel
    {
        //VALIDAR 
        public RolObj? ValidarRol(RolObj rol, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Query<RolObj>("ValidarCredencialesRol",
                    new
                    {
                        rol.Roles

                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }

        }

        //REGISTRAR
        public int Registrar_Rol(RolObj _Rol, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("Crear_Rol",
                    new
                    {
                        _Rol.Roles
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int Actualizar_Rol(RolObj _Rol, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("Editar_Rol",
                    new
                    {
                        _Rol.IdRol,
                        _Rol.Roles
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int Delete_Rol(int _RolId, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                RolObj ROL = new RolObj();
                ROL.IdRol = _RolId;
                return connection.Execute("Borrar_Rol",
                    new
                    {
                        _RolId
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<RolObj> Get_Rol(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<RolObj>("SELECT * from Rol");
                return SqlQuery.ToList();
            }
        }
    }
}
