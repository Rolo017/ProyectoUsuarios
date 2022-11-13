namespace ProyectoTienda_API.Entities
{
    public class UsuarioObj
    {

        public string Usuario { get; set; } = string.Empty;

        public string Nombre { get; set; } = string.Empty;    

        public string Cedula { get; set; } = string.Empty;

        public string Contrasenna { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;
       
        public bool CambioContrasenna { get; set; } = false;

        public bool Activo { get; set; } = false;

        public int TipoUsuario { get; set; } = 0;

        public int IdRol { get; set; } = 0;

        public string Token { get; set; } = string.Empty;


    }
}
