using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.Service.Interface
{
    public interface IPetService
    {
        Task<bool> AlteraAsync(PetDTO obj);
        Task<PetDTO> BuscaPorIdAsync(int id);
        Task<List<PetDTO>> BuscaTodosAsync();
        Task<PetDTO> CriarAsync(PetDTO obj);
        Task<bool> DeletaAsync(PetDTO obj);
    }
}
