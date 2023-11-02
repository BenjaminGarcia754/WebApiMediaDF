using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class VideoType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int IdUsuario { get; set; }

    [Required]
    public int IdVideo { get; set; }

    [Required]
    public string Tipo { get; set; }

    public Usuario Usuario { get; set; }
    public Video Video { get; set; }
}
