using Challenge.Alura.Adopet.API.Dominio;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Challenge.Alura.Adopet.API.DTO
{
    public class TutorDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O Email é obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O Senha é obrigatório")]
        public string? Senha { get; set; }
        public string? Imagem { get; set; }
        [JsonIgnore]
        public ICollection<PetDTO>? Pets { get; set; }
    }
}
