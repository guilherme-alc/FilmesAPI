using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Models;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private static List<Filme> _filmes { get; set; } = new List<Filme>();
        [HttpPost]
        public IActionResult AdicionaFilme([FromBody] Filme filme)
        {
            _filmes.Add(filme);
            return Ok(filme);
        }
    }
}
