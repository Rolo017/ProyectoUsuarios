namespace ProyectoTienda_API.Entities
{
    public class UsuarioObj
    {

        public int Cedula { get; set; } = 0;

        public string Nombre { get; set; } = string.Empty;

        public string Apellidos { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Contrasenna { get; set; } = string.Empty;

        public bool Activo { get; set; } = false;

        public int IdRol { get; set; } = 0;

        public string Token { get; set; } = string.Empty;


    }
}
