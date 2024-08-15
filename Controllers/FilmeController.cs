using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        public FilmeController(FilmeContext context)
        {
            _context = context;
        }

        private FilmeContext _context;

        [HttpPost]
        public IActionResult  CreateFilme ([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = new Filme()
            {
                Titulo = filmeDto.Titulo,
                Diretor = filmeDto.Diretor,
                Genero = filmeDto.Genero,
                Duracao = filmeDto.Duracao,
            };

            _context.Filmes.Add(filme);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetByIdFilme), new { filmeId = filme.Id }, filme);
        }
        [HttpGet]
        public IEnumerable<Filme> GetFilmes()
        {
            return _context.Filmes;
        }

        [HttpGet("{filmeId}")]
        public IActionResult GetByIdFilme(int filmeId) 
        {
            Filme filme = _context.Filmes.Find(filmeId);

            if (filme is null)
                return NotFound("Filme não encontrado.");

            ReadFilmeDto filmeDto = new ReadFilmeDto()
            {
                Id = filmeId,
                Titulo = filme.Titulo,
                Diretor = filme.Diretor,
                Genero = filme.Genero,
                Duracao = filme.Duracao,
                HoraDaConsulta = DateTime.Now,
            };

            return Ok(filmeDto);
        }

        [HttpPut("{filmeId}")]
        public IActionResult UpdateFilme(int filmeId, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.Find(filmeId);

            if (filme is null)
                return NotFound("Filme não encontrado.");

            filme.Titulo = filmeDto.Titulo;
            filme.Diretor = filmeDto.Diretor;
            filme.Genero = filmeDto.Genero;
            filme.Duracao = filmeDto.Duracao;

            _context.Filmes.Update(filme);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpDelete("{filmeId}")]
        public IActionResult DeleteFilme(int filmeId)
        {
            Filme filme = _context.Filmes.Find(filmeId);

            if (filme is null)
                return NotFound("Filme não encontrado.");

            _context.Filmes.Remove(filme);
            _context.SaveChanges(); 

            return NoContent();
        }
    }
}