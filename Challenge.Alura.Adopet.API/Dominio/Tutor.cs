using System.ComponentModel.DataAnnotations;

namespace Challenge.Alura.Adopet.API.Dominio
{
    public class Tutor
    {
      
        [Key]
        public int Id { get; set; }        
        public string? Nome { get; set; }       
        public string? Email { get; set; }      
        public string? Senha { get; set; }        
        public string? NomeDoAnimal { get; set; }
        public string? Imagem { get; set; }
        public ICollection<Pet>? Pets { get; set; }
    }
}
