using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    [Table("usuario_conexao")]
    public class UsuarioConexao
    {
        
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        [Column("id")]
        public int id { get; set; }

        [Required] //not null
        [ForeignKey("Usuario")] //chave estrangeira para a tabela Usuario
        [Column("id_user")]
        public int id_user { get; set; }
        public Usuario Usuario { get; set; } //referencia à classe Usuario

        [Required]
        [ForeignKey("Plataforma")]
        [Column("id_plataforma")]
        public int id_plataforma { get; set; }
        public Plataforma Plataforma { get; set; } //referencia à classe Plataforma

        [Column("status")]
        public bool status { get; set; }

        [Column("link_acesso")]
        public string link_acesso { get; set; }
        
    }
}
