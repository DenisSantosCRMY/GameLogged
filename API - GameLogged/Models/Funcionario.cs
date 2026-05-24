using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Funcionario
    {

        
        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        public int rf { get; set; }

        [MaxLength(100)] 
        public string acesso { get; set; }

        [MaxLength(100)]
        public string nome { get; set; }

        [MaxLength(14)]
        public string cpf { get; set; }

        [MaxLength(100)]
        public string email { get; set; }

        [MaxLength(100)]
        public string password { get; set; }
        
        
    }
}
