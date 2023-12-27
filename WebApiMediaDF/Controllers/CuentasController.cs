using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiMediaDF.Controllers.Services;
using WebApiMediaDF.Modelos.DTOs;

namespace WebApiMediaDF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        private readonly WebApiMediaDbContex _context;
        private readonly IMapper mapper;
        public CuentasController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager,
            WebApiMediaDbContex contex,
            IMapper mapper
        )
        {
            UserManager = userManager;
            Configuration = configuration;
            SignInManager = signInManager;
            this._context = contex;
            this.mapper = mapper;
        }

        public UserManager<IdentityUser> UserManager { get; }
        public IConfiguration Configuration { get; }
        public SignInManager<IdentityUser> SignInManager { get; }

        [HttpPost("registrar")]
        public async Task<ActionResult<RespuestaAutenticacion>> Registrar(CredencialesUsuario credencialesUsuario)
        {
            var usuario = new IdentityUser { UserName = credencialesUsuario.Username };
            var resultado = await UserManager.CreateAsync(usuario, credencialesUsuario.Password);

            if (resultado.Succeeded)
            {
                var usuarioIdentity = await UserManager.FindByNameAsync(credencialesUsuario.Username);
                var userId = usuarioIdentity.Id;
                UsuarioDTO usuarioDTO = new UsuarioDTO()
                {
                    Username = credencialesUsuario.Username,
                    Contraseña = credencialesUsuario.Password,
                    NombreDeUsuario = credencialesUsuario.Nombre,
                    Tipo = credencialesUsuario.Tipo,
                    UsernameIdentity = userId
                };



                try
                {
                    var Usuario = mapper.Map<Usuario>(usuarioDTO);
                    UsuariosServices usuariosServices = new UsuariosServices(_context);
                    usuariosServices.registrarUsuario(Usuario);

                }catch(Exception ex)
                {
                    return BadRequest("Error para construir el objeto" + ex.Message);
                }

                var respuestaAutenticacion = ConstruirToken(credencialesUsuario);

                return respuestaAutenticacion;

            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }


        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login(CredencialesUsuario credencialesUsuario)
        {
            var resultado = await SignInManager.PasswordSignInAsync(credencialesUsuario.Username,
                               credencialesUsuario.Password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return ConstruirToken(credencialesUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> EliminarUsuario(string username)
        {
            var user = await UserManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var resultado = await UserManager.DeleteAsync(user);
            if (resultado.Succeeded)
            {
                UsuariosServices usuariosServices = new UsuariosServices(_context);
                bool respuesta = usuariosServices.EliminarUsuario(user.Id);
                if (!respuesta)
                {
                    return BadRequest("No se pudo eliminar");
                }
                return NoContent();
            }
            else 
            { 
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("Error", error.Description);
                }

                return BadRequest(ModelState);
            }
        }

        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario usuario)
        {
            UsuariosServices usuariosServices = new UsuariosServices(_context);
            var claims = new List<Claim>()
            {
                new Claim("username", usuario.Username)
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);
            var expiracion = DateTime.UtcNow.AddYears(1);

            var securityToken = new JwtSecurityToken(
                               issuer: null,
                               audience: null,
                               claims: claims,
                               expires: expiracion,
                               signingCredentials: creds
                               );
            return new RespuestaAutenticacion()
            {
                Id = usuariosServices.obtenerIdUsuario(usuario),
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion,
                TipoUsuario = usuariosServices.obtenerTipoUsuario(usuario)
            };
        }
    }
}
