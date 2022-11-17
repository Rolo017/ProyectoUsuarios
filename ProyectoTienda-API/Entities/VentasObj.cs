namespace ProyectoTienda_API.Entities
{
    public class VentasObj
    {
        public int ID_Venta { get; set; }
        public int Cedula { get; set; }
        public int Producto { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
    }
}
