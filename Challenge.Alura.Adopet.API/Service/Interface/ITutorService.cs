using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.Service.Interface
{
    public interface ITutorService
    {
        Task<bool> AlteraAsync(TutorDTO obj);
        Task<TutorDTO> BuscaPorIdAsync(int id);
        Task<List<TutorDTO>> BuscaTodosAsync();
        Task<TutorDTO> CriarAsync(TutorDTO obj);
        Task<bool> DeletaAsync(TutorDTO obj);
    }
}
