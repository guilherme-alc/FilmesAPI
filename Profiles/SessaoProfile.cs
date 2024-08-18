using AutoMapper;
using FilmesAPI.Data.Dtos.Sessao;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>()
                .ForMember(dto => dto.HorarioDeInicio, opts => opts
                .MapFrom(dto => dto.Filme != null && dto.Filme.Duracao > 0
                    ? dto.HorarioDeEncerramento.AddMinutes(-dto.Filme.Duracao)
                    : (DateTime?)null));
        }
    }
}
