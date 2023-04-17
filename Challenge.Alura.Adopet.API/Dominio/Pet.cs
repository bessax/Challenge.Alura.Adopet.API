using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Alura.Adopet.API.Dominio
{
    [Table("Pets")]
    public class Pet
    {
       
        public int Id { get; set; }
        public string? Nome { get; set; }
        public Porte Porte { get; set; }
        public string? Descricao { get; set; }
        public int TutorId { get; set; }
        public virtual Tutor? Tutor { get; set; }
        public int AbrigoId { get; set; }
        public virtual Abrigo? Abrigo { get; set; } 

    }
}
