﻿namespace WebApiMediaDF.Modelos.DTOs
{
    public class RespuestaAutenticacion
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
        //public string UserName { get; set; }
        //public string Email { get; set; }
        public int TipoUsuario { get; set; }
    }
}
