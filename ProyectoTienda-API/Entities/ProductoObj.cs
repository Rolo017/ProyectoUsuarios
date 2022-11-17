namespace ProyectoTienda_API.Entities
{


    public class ProductoObj
    {

        public int IdProducto { get; set; } = 0;

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public float Precio { get; set; } = float.MinValue;

        public int Cantidad { get; set; } = 0;



    }





}