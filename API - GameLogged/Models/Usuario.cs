using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key] //indentificação da chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Garante que o valor seja gerado automaticamente (auto-incremento)
        [Column("id")]
        public int id { get; set; }

        [Required] //not null
        [MaxLength(50)] // Corresponde ao VARCHAR(50)
        [Column("nickname")]
        public string nickname { get; set; }

        [Required]
        [MaxLength(100)] 
        [Column("nome")]
        public string nome { get; set; }

        [Required]
        [MaxLength(150)]
        [Column("email")]
        [EmailAddress] // Validação extra do C# para formato de e-mail
        public string email { get; set; }

        [Required]
        [MaxLength(255)]
        [Column("password")]
        public string password { get; set; }

        [MaxLength(200)]
        [Column("foto_perfil")]
        public string foto_perfil { get; set; }

        [MaxLength(200)]
        [Column("banner_perfil")]
        public string banner_perfil { get; set; }

        [Required]
        [Column(TypeName = "date")] // Garante que no MySQL seja DATE e não DATETIME
        public DateTime dt_nasc { get; set; }
    }
}