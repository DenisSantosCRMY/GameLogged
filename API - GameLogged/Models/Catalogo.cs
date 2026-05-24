using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Catalogo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [ForeignKey("Jogo")]
        public int id_jpjogos { get; set; }
        public Jogo Jogo { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int id_user { get; set; }
        public Usuario Usuario { get; set; }

        [MaxLength(50)]
        public string status { get; set; }

        [Column(TypeName = "date")]
        public DateTime dt_adicionado { get; set; }
    }
}
