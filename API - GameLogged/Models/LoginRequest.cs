using System.ComponentModel.DataAnnotations;

namespace back_end.Models
{
    public class LoginRequest
    {
        [Required] //not null
        [EmailAddress] //validação de formato de email
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
