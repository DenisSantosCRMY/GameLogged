using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class UsuarioConexao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int id_user { get; set; }
        public Usuario Usuario { get; set; }

        [Required]
        [ForeignKey("Plataforma")]
        public int id_plataforma { get; set; }
        public Plataforma Plataforma { get; set; }

        public bool status { get; set; }

        public string link_acesso { get; set; }
    }
}
