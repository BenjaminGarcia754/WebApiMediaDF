using Microsoft.EntityFrameworkCore;
using WebApiMediaDF.Modelos.DTOs;

namespace WebApiMediaDF.Controllers.Services
{
    public class UsuariosServices
    {
        public readonly WebApiMediaDbContex _context;
        public UsuariosServices(WebApiMediaDbContex context)
        {
            this._context = context;
        }

        public bool registrarUsuario(Usuario usuario)
        {
            bool respuesta = false;
            try
            {
                _context.Add(usuario);
                _context.SaveChanges();
                respuesta = true;
            }
            catch (Exception ex)
            {
               Console.WriteLine(ex.Message);
               respuesta = false;
            }
            return respuesta;
        }

        public int obtenerIdUsuario(CredencialesUsuario usuario)
        {
            var Usuario = _context.Usuarios.FirstOrDefault(x=> x.Username == usuario.Username);
            return Usuario.Id;
        }

        public int obtenerTipoUsuario(CredencialesUsuario usuario)
        {
            var Usuario = _context.Usuarios.FirstOrDefault(x => x.Username == usuario.Username);
            return Usuario.Tipo;
        }

        public bool EliminarUsuario(string username)
        {
            bool respuesta = false;
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.Username == username);
                _context.Remove(usuario);
                _context.SaveChanges();
                respuesta = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                respuesta = false;
            }
            return respuesta;
        }   

    }
}
