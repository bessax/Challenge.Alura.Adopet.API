using Challenge.Alura.Adopet.API.Dominio;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Challenge.Alura.Adopet.API.DTO
{
    public class PetDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do animal é obrigatório")]
        public string? Nome { get; set; }
        public Porte Porte { get; set; }
        [Required(ErrorMessage = "A descrição do animal é obrigatório")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O Id do Tutor é obrigatório")]
        public int TutorId { get; set; }
        [JsonIgnore]
        public TutorDTO? Tutor { get; set; }    
        public int AbrigoId { get; set; }
        [JsonIgnore]
        public AbrigoDTO? Abrigo { get; set; }
    }
}
