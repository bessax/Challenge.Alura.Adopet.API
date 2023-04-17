using Challenge.Alura.Adopet.API.Dominio;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Alura.Adopet.API.DTO
{
    public class AbrigoDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A nome do abrigo é obrigatório")]
        public string? Nome { get; set; }
        [JsonIgnore]
        public int EnderecoId { get; set; }
        [JsonIgnore]
        public EnderecoDTO? Endereco { get; set; }
        [JsonIgnore]
        public virtual ICollection<PetDTO>? Pets { get; set; }
    }
}
