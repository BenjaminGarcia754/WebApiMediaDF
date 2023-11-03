using Microsoft.Build.Framework;

namespace WebApiMediaDF.DTOs
{
    public class CredencialesUsuario
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
