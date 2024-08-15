using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;
using FilmesAPI.Data;

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
        public IActionResult  CreateFilme ([FromBody] Filme filme)
        {
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
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == filmeId);

            if (filme is null)
                return NotFound("Filme não encontrado.");

            return Ok(filme);
        }
    }
}