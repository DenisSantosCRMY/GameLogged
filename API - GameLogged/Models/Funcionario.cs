using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Funcionario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int rf { get; set; }

        [MaxLength(100)]
        public string acesso { get; set; }

        [MaxLength(100)]
        public string nome { get; set; }

        public int cpf { get; set; }

        public int password { get; set; }

        public int email { get; set; }
    }
}
