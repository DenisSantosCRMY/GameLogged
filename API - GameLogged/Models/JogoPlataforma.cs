using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class JogoPlataforma
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_jpjogos { get; set; }

        [Required]
        [ForeignKey("Jogo")]
        public int id_jogo { get; set; }
        public Jogo Jogo { get; set; }

        [Required]
        [ForeignKey("Plataforma")]
        public int id_plataforma { get; set; }
        public Plataforma Plataforma { get; set; }
    }
}
