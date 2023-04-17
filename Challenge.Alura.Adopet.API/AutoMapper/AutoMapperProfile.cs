using AutoMapper;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tutor,TutorDTO>();
            CreateMap<Pet, PetDTO>();
            CreateMap<Abrigo, AbrigoDTO>().ForMember(a => a.Endereco, r => r.MapFrom(r => new EnderecoDTO()
            {
                Logradouro = r.Endereco.Logradouro,
                CEP = r.Endereco.CEP,
                Cidade = r.Endereco.Cidade,
                Estado = r.Endereco.Estado
                
            })); 
            CreateMap<Endereco, EnderecoDTO>();

            CreateMap<TutorDTO,Tutor>();
            CreateMap<PetDTO,Pet>();
            CreateMap<AbrigoDTO, Abrigo>().ForMember(a => a.Endereco, r => r.MapFrom(r => new Endereco()
            {
                Logradouro = r.Endereco.Logradouro,
                CEP = r.Endereco.CEP,
                Cidade = r.Endereco.Cidade,
                Estado = r.Endereco.Estado

            }));
            CreateMap<EnderecoDTO, Endereco>();
        }
    }

}
