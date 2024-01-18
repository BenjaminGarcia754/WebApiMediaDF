using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Video
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Ruta { get; set; }

    [Required]
    public string Imagen { get; set; }

    [Required]
    public string FechaSubida { get; set; }

    [Required]
    public string FechaModificacion { get; set; }

    [Required]
    public int Materia { get; set; }

    public int? Valoracion { get; set; }

    public Materia MateriaNavigation { get; set; }
    public ICollection<VideoType> VideoTypes { get; set; }
    public ICollection<Comentarios> Comentarios { get; set; }
    public ICollection<Reporte> Reportes { get; set; }
}
