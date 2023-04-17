using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.Service.Interface
{
    public interface IAbrigoService
    {
        Task<bool> AlteraAsync(AbrigoDTO obj);
        Task<AbrigoDTO> BuscaPorIdAsync(int id);
        Task<List<AbrigoDTO>> BuscaTodosAsync();
        Task<AbrigoDTO> CriarAsync(AbrigoDTO obj);
        Task<bool> DeletaAsync(AbrigoDTO obj);
    }
}
