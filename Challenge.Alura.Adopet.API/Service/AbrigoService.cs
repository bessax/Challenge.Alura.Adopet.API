using AutoMapper;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Challenge.Alura.Adopet.API.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.AdoAbrigo.API.Service
{
    public class AbrigoService : IService<AbrigoDTO>
    {
        private readonly IRepository<Abrigo> repository;
        private readonly IMapper mapper;
        public AbrigoService(IRepository<Abrigo> repository, IMapper _mapper)
        {
            this.repository = repository;
            this.mapper = _mapper;
        }
        public async Task<bool> AlteraAsync(AbrigoDTO obj)
        {
            var abrigo = this.mapper.Map<Abrigo>(obj);
            try
            {
                await this.repository.AlteraAsync(abrigo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.AbrigoExists(abrigo.Id))
                {
                    return false;
                }
                else
                {
                    throw new Exception("Objeto não encontrado para atualização.");
                }
            }

            return true;
        }

        private async Task<bool> AbrigoExists(int id)
        {
            var abrigos = await this.repository.BuscaTodosAsync();
            return (abrigos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<AbrigoDTO> BuscaPorIdAsync(int id)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return null;
            }

            var abrigo = await this.repository.BuscaPorIdAsync(id);

            if (abrigo == null)
            {
                return null;
            }
            return this.mapper.Map<AbrigoDTO>(abrigo);
        }

        public async Task<List<AbrigoDTO>> BuscaTodosAsync()
        {
            var abrigos = await this.repository.BuscaTodosAsync();
            return this.mapper.Map<List<AbrigoDTO>>(abrigos);
        }

        public async Task<AbrigoDTO> CriarAsync(AbrigoDTO obj)
        {
            var abrigo = this.mapper.Map<Abrigo>(obj);
            await this.repository.CriarAsync(abrigo);
            return this.mapper.Map<AbrigoDTO>(abrigo);
        }

        public async Task<bool> DeletaAsync(AbrigoDTO obj)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return false;
            }

            var abrigo = await this.repository.BuscaPorIdAsync(obj.Id);

            if (abrigo == null)
            {
                return false;
            }
            await this.repository.DeletaAsync(abrigo);
            return true;
        }
    }
}
