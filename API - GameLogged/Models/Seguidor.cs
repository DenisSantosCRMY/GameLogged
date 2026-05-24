using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Seguidor
    {
        
        [Key, Column(Order = 0)] //chave primária composta
        [ForeignKey("Seguidor")] //chave estrangeira para a tabela Usuario
        public int id_seguidor { get; set; }
        public Usuario UsuarioSeguidor { get; set; } //referencia à classe Usuario para o seguidor

        [Key, Column(Order = 1)]
        [ForeignKey("Seguindo")]
        public int id_seguindo { get; set; }
        public Usuario UsuarioSeguindo { get; set; } //referencia à classe Usuario para o seguindo
        
    }
}
