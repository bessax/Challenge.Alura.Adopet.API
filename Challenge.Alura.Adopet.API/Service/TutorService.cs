using AutoMapper;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Challenge.Alura.Adopet.API.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Service
{
    public class TutorService : IService<TutorDTO>
    {
        private readonly IRepository<Tutor> repository;
        private readonly IMapper mapper;
        public TutorService(IRepository<Tutor> repository, IMapper _mapper)
        {
            this.repository = repository;
            this.mapper = _mapper;  
        }
        public async Task<bool> AlteraAsync(TutorDTO obj)
        {
            var tutor = this.mapper.Map<Tutor>(obj);
            try
            {
                await this.repository.AlteraAsync(tutor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.TutorExists(tutor.Id))
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

        private async Task<bool> TutorExists(int id)
        {
            var agencias = await this.repository.BuscaTodosAsync();
            return (agencias?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<TutorDTO> BuscaPorIdAsync(int id)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return null;
            }

            var tutor = await this.repository.BuscaPorIdAsync(id);

            if (tutor == null)
            {
                return null;
            }
            return this.mapper.Map<TutorDTO>(tutor);
        }

        public async Task<List<TutorDTO>> BuscaTodosAsync()
        {
            var tutores = await this.repository.BuscaTodosAsync();
            return this.mapper.Map<List<TutorDTO>>(tutores);
        }

        public async Task<TutorDTO> CriarAsync(TutorDTO obj)
        {
            var tutor = this.mapper.Map<Tutor>(obj);
            await this.repository.CriarAsync(tutor);
            return this.mapper.Map<TutorDTO>(tutor);
        }

        public async Task<bool> DeletaAsync(TutorDTO obj)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return false;
            }

            var agencia = await this.repository.BuscaPorIdAsync(obj.Id);

            if (agencia == null)
            {
                return false;
            }
            await this.repository.DeletaAsync(agencia);
            return true;
        }
    }
}
