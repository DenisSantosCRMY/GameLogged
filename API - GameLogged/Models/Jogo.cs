using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    [Table("jogo")]
    public class Jogo
    {
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        [Column("id")]
        public int id { get; set; }

        [Required] //not null
        [MaxLength(255)]
        [Column("nome")]
        public string nome { get; set; }

        [Column("banner_jogo")]
        public string banner_jogo { get; set; }
    }
}
