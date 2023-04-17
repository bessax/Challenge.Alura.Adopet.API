using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Challenge.Alura.Adopet.API.Dominio
{
    [Table("Enderecos")]
    public class Endereco
    {
        [Key]
        public int Id { get; set; }
        public string? CEP { get; set; }
        public string? Logradouro { get; set; }
        public string? Cidade { get; set; }
        public string? Estado { get; set; }
        public virtual Abrigo? Abrigo { get; set; }
    }
}