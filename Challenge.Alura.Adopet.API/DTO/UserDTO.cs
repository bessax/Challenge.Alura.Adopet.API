using System.ComponentModel.DataAnnotations;

namespace Challenge.Alura.Adopet.API.DTO
{
    public class UserDTO
    {
        [Required(ErrorMessage = "O E-mail é obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatório")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "A Confirmação de senha é obrigatório")]
        public string? ConfirmPassaword { get; set; }
    }
}
