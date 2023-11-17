using WebApiMediaDF.Modelos.DTOs.StaticDTO;

public class UsuarioDTO
{
    public int Id { get; set; }
    public string Username { get; set; }
    public int Tipo { get; set; }
    public string Contraseña { get; set; }
    public string NombreDeUsuario { get; set; }

    public string UsernameIdentity { get; set; }

    public bool registrarUsuario()
    {
        try
        {
            using (var db = new WebApiMediaDbContex())
            {
                var usuario = new Usuario()
                {
                    Username = this.Username,
                    Tipo = this.Tipo,
                    Contraseña = this.Contraseña,
                    NombreDeUsuario = this.NombreDeUsuario,
                    IdUsuarioIdentity = UsernameIdentity
                };
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return true;
            }
        }
        catch (Exception)
        {
            return false;
        }
    }
}
