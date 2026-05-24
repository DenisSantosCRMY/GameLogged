using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Catalogo
    {
        
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        public int id { get; set; }

        [Required] //corresponde ao NN (Not Null)
        [ForeignKey("Jogo")] //chave estrangeira
        public int id_jpjogos { get; set; }
        public Jogo Jogo { get; set; } //referencia à classe Jogo

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
