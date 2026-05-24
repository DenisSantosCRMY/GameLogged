using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class UsuarioConquista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int id_user { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        [ForeignKey("Conquista")]
        public int id_conquista { get; set; }
        public Conquista Conquista { get; set; }

        [Column(TypeName = "date")]
        public DateTime dt_desbloqueio { get; set; }
    }
}
