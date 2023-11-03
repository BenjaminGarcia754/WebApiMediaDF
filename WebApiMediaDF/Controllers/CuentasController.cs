using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiMediaDF.DTOs;

namespace WebApiMediaDF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuentasController : ControllerBase
    {
        public CuentasController(UserManager<IdentityUser> userManager,
            IConfiguration configuration,
            SignInManager<IdentityUser> signInManager
        )
        {
            UserManager = userManager;
            Configuration = configuration;
            SignInManager = signInManager;
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
                return ConstruirToken(credencialesUsuario);
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

        private RespuestaAutenticacion ConstruirToken(CredencialesUsuario usuario)
        {
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
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiracion
            };
        }
    }
}
