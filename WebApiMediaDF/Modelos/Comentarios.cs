using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Comentarios
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int VideoRelacionado { get; set; }

    [Required]
    public string Comentario { get; set; }

    public Video VideoRelacionadoNavigation { get; set; }
}
