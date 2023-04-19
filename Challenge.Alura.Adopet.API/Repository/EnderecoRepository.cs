using Challenge.Alura.Adopet.API.Data;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Repository
{
    public class EnderecoRepository : IRepository<Endereco>
    {
        private readonly AdoPetContext _repository;
        public EnderecoRepository(AdoPetContext repository)
        {
            _repository = repository;
        }
        public async Task AlteraAsync(Endereco obj)
        {
            _repository.Enderecos.Update(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task<Endereco> BuscaPorIdAsync(int id)
        {
            return await _repository.Enderecos.FirstAsync(a => a.Id == id);
        }

        public async Task<List<Endereco>> BuscaTodosAsync()
        {
            return await _repository.Enderecos.ToListAsync();
                 
        }

        public async Task CriarAsync(Endereco obj)
        {
            _repository.Enderecos.Add(obj);
            await _repository.SaveChangesAsync();
        }

        public async Task DeletaAsync(Endereco obj)
        {
            _repository.Enderecos.Remove(obj);
            await _repository.SaveChangesAsync();
        }
    }
}
