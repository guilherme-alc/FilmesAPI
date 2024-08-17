using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>()
                .ForMember(dto => dto.HoraDaConsulta, opts => opts
                .MapFrom(src => DateTime.Now));
        }
    }
}
