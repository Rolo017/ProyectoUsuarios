using Dapper;
using ProyectoTienda_API.Entities;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTienda_API.Models
{
    public class ErrorModel
    {
        public List<ErrorObj> Get_Errores(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<ErrorObj>("SELECT * from Errores");
                return SqlQuery.ToList();
            }
        }
        public int EliminarError(int ID_Error, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                ErrorObj _Error = new ErrorObj();
                _Error.IdError = ID_Error;

                return connection.Execute("BorrarError",
                    new { _Error.IdError }, commandType: CommandType.StoredProcedure);
            }
        }
        public int EliminarErrores(int ID_Venta, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {

                return connection.Execute("DELETE FROM Errores");
            }
        }
    }
}
