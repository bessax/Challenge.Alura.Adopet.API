namespace Challenge.Alura.Adopet.API.Service.Interface
{
    public interface IService<T>
    {
        Task<bool> AlteraAsync(T obj);
        Task<T> BuscaPorIdAsync(int id);
        Task<List<T>> BuscaTodosAsync();
        Task<T> CriarAsync(T obj);
        Task<bool> DeletaAsync(T obj);
    }
}
