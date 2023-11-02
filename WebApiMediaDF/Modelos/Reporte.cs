using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Reporte
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int UsuarioReporte { get; set; }

    [Required]
    public int VideoReporte { get; set; }

    [Required]
    public int TipoReporte { get; set; }

    public Usuario UsuarioReporteNavigation { get; set; }
    public Video VideoReporteNavigation { get; set; }
    public TipoReporte TipoReporteNavigation { get; set; }
}
