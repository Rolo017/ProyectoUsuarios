using Dapper;
using ProyectoTienda_API.Entities;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoTienda_API.Models
{
    public class ProductoModel
    {
        public ProductoObj? ValidarProducto(ProductoObj producto, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Query<ProductoObj>("ValidarCredencialesProducto",
                    new {
                        producto.Nombre
                        
                    }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }


        public int Registrar_Producto(ProductoObj _Producto, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                return connection.Execute("RegistrarProducto",
                    new
                    {
                        _Producto.Nombre,
                        _Producto.IdProducto,
                        _Producto.Precio,
                        _Producto.Descripcion,
                        _Producto.Cantidad
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int Actualizar_Producto(int IdProducto, ProductoObj _Producto, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {

                _Producto.IdProducto = IdProducto;
                return connection.Execute("EditarProducto",
                    new
                    {
                        _Producto.Nombre,
                        _Producto.IdProducto,
                        _Producto.Precio,
                        _Producto.Descripcion,
                        _Producto.Cantidad
                    }, commandType: CommandType.StoredProcedure);
            }
        }

        public int Borrar_Producto(int IdProducto, IConfiguration stringConnection)
        {
            using (var connection = new SqlConnection(stringConnection.GetSection("ConnectionStrings:Connection").Value))
            {
                ProductoObj _Producto = new ProductoObj();
                _Producto.IdProducto = IdProducto;

                return connection.Execute("BorrarProducto",
                    new
                    {
                        _Producto.IdProducto
                    }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
