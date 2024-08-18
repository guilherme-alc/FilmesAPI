using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class FilmeServices
    {

        private AppDbContext _context;
        private IMapper _mapper;
        public FilmeServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public ReadFilmeDto AdicionaFilme(CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return _mapper.Map<ReadFilmeDto>(filme);
        }
        public List<ReadFilmeDto> RecuperaFilmes(int? duracao = null)
        {
            IQueryable<Filme> query = _context.Filmes;

            if (duracao.HasValue)
            {
                query = query.Where(filme => filme.Duracao <= duracao.Value);
            }

            List<Filme> filmes = query.ToList();
            List<ReadFilmeDto> filmesDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
            return filmesDto;
            /*
            List<Filme> filmes;

            if (duracao is null)
                filmes = _context.Filmes.ToList();
            else
                filmes = _context.Filmes.Where(filme => filme.Duracao <= duracao).ToList();
            List<ReadFilmeDto> filmesDto = _mapper.Map<List<ReadFilmeDto>>(filmes);
            return filmesDto;*/
        }
        public ReadFilmeDto RecuperarFilmePorId(int id) 
        {
            Filme filme = _context.Filmes.Find(id);

            if (filme is null)
                return null;

            return _mapper.Map<ReadFilmeDto>(filme);
        }
        public Result AtualizaFilme(int id, UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.Find(id);

            if (filme is null)
                return Result.Fail("Filme não encontrado.");

            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();

            return Result.Ok();
        }
        public Result DeletaFilme(int id)
        {
            Filme filme = _context.Filmes.Find(id);

            if (filme is null)
                return Result.Fail("Filme não encontrado.");

            _context.Filmes.Remove(filme);
            _context.SaveChanges();

            return Result.Ok();
        }
    }
}
