using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    [Table("plataforma")]
    public class Plataforma
    {

        [Key] //chave primária
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto-incremento
        [Column("id")]
        public int id { get; set; }

        [Column("nome")]
        public string nome { get; set; }

        [Column("descricao")]
        public string descricao { get; set; }

        // Define se a plataforma está ativa
        // true = ativa
        // false = inativa
        [Column("ativo")]
        public bool ativo { get; set; }
    }
}