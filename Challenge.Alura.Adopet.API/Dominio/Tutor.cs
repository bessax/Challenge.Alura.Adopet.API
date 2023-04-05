using System.ComponentModel.DataAnnotations;

namespace Challenge.Alura.Adopet.API.Dominio
{
    public class Tutor
    {
      
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O Senha é obrigatório")]
        public string? Senha { get; set; }
        [Required(ErrorMessage = "O nome do animal é obrigatório")]
        public string? NomeDoAnimal { get; set; }
        public string? Imagem { get; set; }
    }
}
