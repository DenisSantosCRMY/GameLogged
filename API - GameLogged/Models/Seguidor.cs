using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    public class Seguidor
    {
        [Key, Column(Order = 0)]
        [ForeignKey("Seguidor")]
        public int id_seguidor { get; set; }
        public Usuario UsuarioSeguidor { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Seguindo")]
        public int id_seguindo { get; set; }
        public Usuario UsuarioSeguindo { get; set; }
    }
}
