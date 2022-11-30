using Dapper;
using ProyectoTienda_API.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTienda_API.Models
{
    public class VentasModel
    {
        public VentasObj? ValidarVentas(VentasObj ventas, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Query<VentasObj>("ValidarCredencialesProducto",
                    new
                    {
                        ventas.Producto
                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int RegistrarVenta(VentasObj _venta, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("CrearVenta",
                    new
                    {
                        _venta.ID_Venta,
                        _venta.Cedula,
                        _venta.Producto,
                        _venta.Precio,
                        _venta.Cantidad,
                        _venta.Descripcion
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int ActualizarVenta(VentasObj _venta, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("EditarVenta",
                    new
                    {
                        _venta.ID_Venta,
                        _venta.Cedula,
                        _venta.Producto,
                        _venta.Precio,
                        _venta.Cantidad,
                        _venta.Descripcion
                    }, commandType: CommandType.StoredProcedure);
            }
        }


        public int EliminarVenta(int ID_Venta, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                VentasObj _Venta = new VentasObj();
                _Venta.ID_Venta = ID_Venta;

                return connection.Execute("BorrarVenta",
                    new { _Venta.ID_Venta }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<VentasObj> Get_Ventas(IConfiguration stringConnection)
        {

            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                var SqlQuery = connection.Query<VentasObj>("SELECT * from ventas");
                return SqlQuery.ToList();
            }
        }
    }
}
