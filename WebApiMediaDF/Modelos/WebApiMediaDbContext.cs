using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class WebApiMediaDbContex : IdentityDbContext
{
    public DbSet<TipoUsuario> TipoUsuarios { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Video> Videos { get; set; }
    public DbSet<VideoType> VideoTypes { get; set; }
    public DbSet<TipoReporte> TiposReporte { get; set; }
    public DbSet<Reporte> Reportes { get; set; }
    public DbSet<Comentarios> Comentarios { get; set; }

    public WebApiMediaDbContex(DbContextOptions<WebApiMediaDbContex> options) : base(options)
    {

    }

    public WebApiMediaDbContex()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TipoUsuario>().HasData(
                       new TipoUsuario { Id = 1, Tipo = "Administrador" },
                                  new TipoUsuario { Id = 2, Tipo = "Usuario" }
                                         );
    }
}
