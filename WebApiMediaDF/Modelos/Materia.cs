using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Materia
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required]
    public string Ruta { get; set; }

    public ICollection<Video> Videos { get; set; }
}
