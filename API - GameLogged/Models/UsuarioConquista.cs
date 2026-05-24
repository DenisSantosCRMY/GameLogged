using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class UsuarioConquista
    {
        
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        public int id { get; set; }

        [Required] //not null
        [ForeignKey("Usuario")] //chave estrangeira para a tabela Usuario
        public int id_user { get; set; }
        public Usuario Usuario { get; set; } //referencia à classe Usuario

        [Required]
        [ForeignKey("Conquista")]
        public int id_conquista { get; set; }
        public Conquista Conquista { get; set; } //referencia à classe Conquista

        [Column(TypeName = "date")] //formato de data
        public DateTime dt_desbloqueio { get; set; } //data de desbloqueio da conquista
        
    }
}
