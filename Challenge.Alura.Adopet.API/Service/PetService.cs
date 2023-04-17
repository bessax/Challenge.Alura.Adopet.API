using AutoMapper;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Challenge.Alura.Adopet.API.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.Adopet.API.Service
{
    public class PetService : IPetService
    {
        private readonly IRepository<Pet> repository;
        private readonly IMapper mapper;
        public PetService(IRepository<Pet> repository, IMapper _mapper)
        {
            this.repository = repository;
            this.mapper = _mapper;
        }
        public async Task<bool> AlteraAsync(PetDTO obj)
        {
            var pet = this.mapper.Map<Pet>(obj);
            try
            {
                await this.repository.AlteraAsync(pet);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.PetExists(pet.Id))
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

        private async Task<bool> PetExists(int id)
        {
            var pets = await this.repository.BuscaTodosAsync();
            return (pets?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<PetDTO> BuscaPorIdAsync(int id)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return null;
            }

            var pet = await this.repository.BuscaPorIdAsync(id);

            if (pet == null)
            {
                return null;
            }
            return this.mapper.Map<PetDTO>(pet);
        }

        public async Task<List<PetDTO>> BuscaTodosAsync()
        {
            var pets = await this.repository.BuscaTodosAsync();
            return this.mapper.Map<List<PetDTO>>(pets);
        }

        public async Task<PetDTO> CriarAsync(PetDTO obj)
        {
            var pet = this.mapper.Map<Pet>(obj);
            await this.repository.CriarAsync(pet);
            return this.mapper.Map<PetDTO>(pet);
        }

        public async Task<bool> DeletaAsync(PetDTO obj)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return false;
            }

            var pet = await this.repository.BuscaPorIdAsync(obj.Id);

            if (pet == null)
            {
                return false;
            }
            await this.repository.DeletaAsync(pet);
            return true;
        }
    }
}
