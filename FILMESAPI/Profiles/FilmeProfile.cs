using AutoMapper;
using FilmesAPI.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {// Criação de um mapeamento onde eu falo a class que será passado para usuario e class base
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>().ForMember(filmeDto  => filmeDto.Sessao, opt => opt.MapFrom(filme => filme.Sessao));



        }
    }
}
