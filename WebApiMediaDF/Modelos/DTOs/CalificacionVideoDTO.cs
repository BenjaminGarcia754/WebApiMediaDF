namespace WebApiMediaDF.Modelos.DTOs
{
    public class CalificacionVideoDTO
    {
        public int Id { get; set; }
        public int CalificacionUsuario { get; set; }
        public int VideoRelacionado { get; set; }
        public int UsuarioRelacionado { get; set; }
    }
}
