using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using AutoMapper;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private FilmeContext _context;
        private IMapper _mapper;

        [HttpPost]
        public IActionResult  CreateFilme ([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);

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

            ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);

            return Ok(filmeDto);
        }

        [HttpPut("{filmeId}")]
        public IActionResult UpdateFilme(int filmeId, [FromBody] UpdateFilmeDto filmeDto)
        {
            Filme filme = _context.Filmes.Find(filmeId);

            if (filme is null)
                return NotFound("Filme não encontrado.");

            _mapper.Map(filmeDto, filme);

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