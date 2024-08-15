using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> _filmes { get; set; } = new List<Filme>();
        private static int _id = 1;
        [HttpPost]
        public IActionResult  CreateFilme ([FromBody] Filme filme)
        {
            filme.Id = _id++;
            _filmes.Add(filme);
            return CreatedAtAction(nameof(GetByIdFilme), new { filmeId = filme.Id }, filme);
        }
        [HttpGet]
        public IActionResult GetFilmes()
        {
            return Ok(_filmes);
        }
        [HttpGet("{filmeId}")]
        public IActionResult GetByIdFilme(int filmeId) 
        {
            Filme filme = _filmes.FirstOrDefault(filme => filme.Id == filmeId);

            if (filme is null)
                return NotFound("Filme não encontrado.");

            return Ok(filme);
        }
    }
}