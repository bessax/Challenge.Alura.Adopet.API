using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Repository
{
    public class AbrigoRepository : IRepository<Abrigo>
    {
        private readonly AdoPetContext _repository;
        public AbrigoRepository(AdoPetContext repository)
        {
            _repository = repository;
        }
        public async Task AlteraAsync(Abrigo obj)
        {
            _repository.Abrigos.Update(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task<Abrigo> BuscaPorIdAsync(int id)
        {
            return await _repository.Abrigos.Include(a => a.Pets).Include(b=>b.Endereco).FirstAsync(a => a.Id == id);
        }

        public async Task<List<Abrigo>> BuscaTodosAsync()
        {
            return await _repository.Abrigos.Include(a => a.Pets).Include(b=>b.Endereco)
                  .ToListAsync();
        }

        public async Task CriarAsync(Abrigo obj)
        {
            _repository.Abrigos.Add(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletaAsync(Abrigo obj)
        {
            _repository.Abrigos.Remove(obj);
            await _repository.SaveChangesAsync();
        }
    }
}
