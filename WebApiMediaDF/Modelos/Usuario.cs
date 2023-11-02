using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public int Tipo { get; set; }

    [Required]
    public string Contraseña { get; set; }

    [Required]
    public string NombreDeUsuario { get; set; }

    public TipoUsuario TipoUsuario { get; set; }
    public ICollection<VideoType> VideoTypes { get; set; }
    public ICollection<Reporte> Reportes { get; set; }
}
