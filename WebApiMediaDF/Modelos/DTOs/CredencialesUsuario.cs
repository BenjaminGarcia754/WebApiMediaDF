using Microsoft.Build.Framework;

namespace WebApiMediaDF.Modelos.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Nombre { get; set; }
        public int Tipo { get; set; }

    }
}
