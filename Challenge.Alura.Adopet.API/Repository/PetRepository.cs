using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Repository
{
    public class PetRepository : IRepository<Pet>
    {
        private readonly AdoPetContext _repository;
        public PetRepository(AdoPetContext repository)
        {
            _repository = repository;
        }
        public async Task AlteraAsync(Pet obj)
        {
            _repository.Pets.Update(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task<Pet> BuscaPorIdAsync(int id)
        {
            return await _repository.Pets.Include(a => a.Tutor).Include(b=>b.Abrigo).FirstAsync(a => a.Id == id);
        }

        public async Task<List<Pet>> BuscaTodosAsync()
        {
            return await _repository.Pets.Include(a => a.Tutor).Include(b => b.Abrigo)
                  .ToListAsync();
        }

        public async Task CriarAsync(Pet obj)
        {
            _repository.Pets.Add(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletaAsync(Pet obj)
        {
            _repository.Pets.Remove(obj);
            await _repository.SaveChangesAsync();
        }
    }
}
