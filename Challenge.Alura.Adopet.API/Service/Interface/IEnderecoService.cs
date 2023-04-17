using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.Service.Interface
{
    public interface IEnderecoService
    {
        Task<bool> AlteraAsync(EnderecoDTO obj);
        Task<EnderecoDTO> BuscaPorIdAsync(int id);
        Task<List<EnderecoDTO>> BuscaTodosAsync();
        Task<EnderecoDTO> CriarAsync(EnderecoDTO obj);
        Task<bool> DeletaAsync(EnderecoDTO obj);
    }
}
