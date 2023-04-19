using AutoMapper;
using Challenge.Alura.Adopet.API.Dominio;
using Challenge.Alura.Adopet.API.DTO;

namespace Challenge.Alura.Adopet.API.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tutor,TutorDTO>().ForMember(x=>x.Senha,x=>x.Ignore());
            CreateMap<Pet, PetDTO>()
                .ForMember(a => a.Tutor,
                             r => r.MapFrom(r => new TutorDTO()
                             {
                                 //Id=r.Tutor.Id,
                                 Imagem = r.Tutor.Imagem,
                                 Email = r.Tutor.Email,
                                 Nome = r.Tutor.Nome,
                             }
                             ))
                .ForMember(a => a.Abrigo,
                r => r.MapFrom(r => new AbrigoDTO()
                {                
                    Nome= r.Abrigo.Nome,                  
                }));
            CreateMap<Abrigo, AbrigoDTO>().ForMember(a => a.Endereco, r => r.MapFrom(r => new EnderecoDTO()
            {
                //Id=r.Endereco.Id,
                Logradouro = r.Endereco.Logradouro,
                CEP = r.Endereco.CEP,
                Cidade = r.Endereco.Cidade,
                Estado = r.Endereco.Estado
                
            })); 
            CreateMap<Endereco, EnderecoDTO>();

            CreateMap<TutorDTO,Tutor>();
            CreateMap<PetDTO,Pet>()
                .ForMember(a => a.Tutor,
                             r => r.MapFrom(r => new Tutor()
                             {
                                 //Id = r.Tutor.Id,
                                 Imagem = r.Tutor.Imagem,
                                 Email = r.Tutor.Email,
                                 Nome = r.Tutor.Nome,
                             }
                             ))
                .ForMember(a => a.Abrigo,
                r => r.MapFrom(r => new Abrigo()
                {
                    Id = r.Abrigo.Id,
                    Nome = r.Abrigo.Nome,
                }));
            CreateMap<AbrigoDTO, Abrigo>().ForMember(a => a.Endereco, r => r.MapFrom(r => new Endereco()
            {
                //Id=r.Endereco.Id,
                Logradouro = r.Endereco.Logradouro,
                CEP = r.Endereco.CEP,
                Cidade = r.Endereco.Cidade,
                Estado = r.Endereco.Estado

            }));
            CreateMap<EnderecoDTO, Endereco>();
        }
    }

}
