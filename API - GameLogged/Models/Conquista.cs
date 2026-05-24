using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Conquista
    {
        
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        public int id { get; set; }

        [Required] //not null
        [ForeignKey("Jogo")] //chave estrangeira para a tabela Jogo
        public int id_jp { get; set; }
        public Jogo Jogo { get; set; } //referencia à classe Jogo

        [MaxLength(255)]
        public string titulo { get; set; }

        public string descricao { get; set; }

        public string banner_conquista { get; set; }

        public int score { get; set; }
        
    }
}
