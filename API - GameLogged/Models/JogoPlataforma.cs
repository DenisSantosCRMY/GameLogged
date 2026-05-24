using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class JogoPlataforma
    {
        
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        public int id_jpjogos { get; set; }

        [Required] //not null
        [ForeignKey("Jogo")] //chave estrangeira para a tabela Jogo
        public int id_jogo { get; set; }
        public Jogo Jogo { get; set; } //referencia à classe Jogo

        [Required]
        [ForeignKey("Plataforma")]
        public int id_plataforma { get; set; }
        public Plataforma Plataforma { get; set; } //referencia à classe Plataforma
        
        
    }
}
