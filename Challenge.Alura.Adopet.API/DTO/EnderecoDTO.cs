using Challenge.Alura.Adopet.API.Dominio;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Challenge.Alura.Adopet.API.DTO
{
    public class EnderecoDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required(ErrorMessage = "O Cep é obrigatório")]
        public string? CEP { get; set; }
        [Required(ErrorMessage = "O nome da rua é obrigatório")]
        public string? Logradouro { get; set; }
        [Required(ErrorMessage = "A cidade é obrigatório")]
        public string? Cidade { get; set; }
        [Required(ErrorMessage = "O nome do estado é obrigatório")]
        public string? Estado { get; set; }
       
    }
}
