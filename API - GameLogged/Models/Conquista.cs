using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Conquista
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [ForeignKey("Jogo")]
        public int id_jp { get; set; }
        public Jogo Jogo { get; set; }

        [MaxLength(255)]
        public string titulo { get; set; }

        public string descricao { get; set; }

        public string banner_conquista { get; set; }

        public int score { get; set; }
    }
}
