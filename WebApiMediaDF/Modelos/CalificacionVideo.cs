using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiMediaDF.Modelos
{
    public class CalificacionVideo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CalificacionUsuario { get; set; }
        [Required]
        public int VideoRelacionado { get; set; }
        [Required]
        public int UsuarioRelacionado { get; set; }

        public Usuario Usuario { get; set; }

        public Video Video { get; set; }
    }
}
