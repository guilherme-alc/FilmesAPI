using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using FluentResults;

namespace FilmesAPI.Services
{
    public class CinemaServices
    {
        private AppDbContext _context;
        private IMapper _mapper;
        public CinemaServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReadCinemaDto AdicionaCinema(CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return _mapper.Map<ReadCinemaDto>(cinema);
        }

        public ReadCinemaDto RecuperaCinemaPorId(int id)
        {
            Cinema cinema = _context.Cinemas.Find(id);
            if (cinema != null)
            {
                ReadCinemaDto cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            }
            return null;
        }
        public List<ReadCinemaDto> RecuperaCinemas(string nomeDoFilme)
        {
            IQueryable<Cinema> cinemasQuery = _context.Cinemas;

            if (!string.IsNullOrEmpty(nomeDoFilme))
            {
                cinemasQuery = cinemasQuery.Where(cinema =>
                    cinema.Sessoes.Any(sessao => sessao.Filme.Titulo.Equals(nomeDoFilme)));
            }
            List<Cinema> cinemas = cinemasQuery.ToList();
            return _mapper.Map<List<ReadCinemaDto>>(cinemas);
        }
        public Result AtualizaCinema(int id, UpdateCinemaDto cinemaDto)
        {
            Cinema cinema = _context.Cinemas.Find(id);
            if (cinema == null)
                return Result.Fail("Filme não encontrado.");
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return Result.Ok();
        }
        public Result DeletaCinema(int id)
        {
            Cinema cinema = _context.Cinemas.Find(id);
            if (cinema == null)
                return Result.Fail("Cinema não encontrado.");
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}
