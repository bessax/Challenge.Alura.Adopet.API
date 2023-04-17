using AutoMapper;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;
using Challenge.Alura.Adopet.API.Repository.Interface;
using Challenge.Alura.Adopet.API.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace Challenge.Alura.AdoEndereco.API.Service
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IRepository<Endereco> repository;
        private readonly IMapper mapper;
        public EnderecoService(IRepository<Endereco> repository, IMapper _mapper)
        {
            this.repository = repository;
            this.mapper = _mapper;
        }
        public async Task<bool> AlteraAsync(EnderecoDTO obj)
        {
            var endereco = this.mapper.Map<Endereco>(obj);
            try
            {
                await this.repository.AlteraAsync(endereco);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await this.EnderecoExists(endereco.Id))
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

        private async Task<bool> EnderecoExists(int id)
        {
            var enderecos = await this.repository.BuscaTodosAsync();
            return (enderecos?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public async Task<EnderecoDTO> BuscaPorIdAsync(int id)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return null;
            }

            var endereco = await this.repository.BuscaPorIdAsync(id);

            if (endereco == null)
            {
                return null;
            }
            return this.mapper.Map<EnderecoDTO>(endereco);
        }

        public async Task<List<EnderecoDTO>> BuscaTodosAsync()
        {
            var enderecos = await this.repository.BuscaTodosAsync();
            return this.mapper.Map<List<EnderecoDTO>>(enderecos);
        }

        public async Task<EnderecoDTO> CriarAsync(EnderecoDTO obj)
        {
            var endereco = this.mapper.Map<Endereco>(obj);
            await this.repository.CriarAsync(endereco);
            return this.mapper.Map<EnderecoDTO>(endereco);
        }

        public async Task<bool> DeletaAsync(EnderecoDTO obj)
        {
            if (await this.repository.BuscaTodosAsync() == null)
            {
                return false;
            }

            var endereco = await this.repository.BuscaPorIdAsync(obj.Id);

            if (endereco == null)
            {
                return false;
            }
            await this.repository.DeletaAsync(endereco);
            return true;
        }
    }
}
