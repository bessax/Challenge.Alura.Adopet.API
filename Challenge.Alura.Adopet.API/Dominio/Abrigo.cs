using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Alura.Adopet.API.Dominio
{
    [Table("Abrigos")]
    public class Abrigo
    {
        [Key]
        public int Id { get; set; }
        public string? Nome{ get; set; }        
        public virtual ICollection<Pet>? Pets { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco? Endereco { get; set; }
    }
}
