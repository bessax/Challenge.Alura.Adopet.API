using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Repository
{
    public class TutorRepository : IRepository<Tutor>
    {
        private readonly AdoPetContext _repository;
        public TutorRepository(AdoPetContext repository)
        {
            _repository = repository;
        }
        public async Task AlteraAsync(Tutor obj)
        {
            _repository.Tutores.Update(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task<Tutor> BuscaPorIdAsync(int id)
        {
            return await _repository.Tutores.Include(a => a.Pets).FirstAsync(a => a.Id == id);
        }

        public async Task<List<Tutor>> BuscaTodosAsync()
        {
            return await _repository.Tutores.Include(a => a.Pets)
                  .ToListAsync();
        }

        public async Task CriarAsync(Tutor obj)
        {
            _repository.Tutores.Add(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletaAsync(Tutor obj)
        {
            _repository.Tutores.Remove(obj);
            await _repository.SaveChangesAsync();
        }
    }
}
